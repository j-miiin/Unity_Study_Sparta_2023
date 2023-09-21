using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIPlayerInventory : GameUIClass
{
    const int CONTAINER_NUM = 20;

    [SerializeField] private GameObject _playerInventoryBackgroundImage;
    [SerializeField] private Button _closeInventoryButton;
    [SerializeField] private GameObject _contentContainer;
    [SerializeField] private GameObject _itemDescriptionImage;
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemEffectText;

    private Animator _animator;

    private UIController _controller;

    // �κ��丮 �ݱ� ��ư�� Ŭ�� ������ ����
    private void Awake()
    {
        transform.localScale = Vector3.one;
        _animator = _itemDescriptionImage.GetComponent<Animator>();
    }

    private void Start()
    {
        _controller = UIManager.Instance.controller;
        _controller.OnOpenPlayerInventoryEvent += OpenUI;
        _controller.OnClosePlayerInventoryEvent += CloseUI;
        _controller.OnShowItemDescEvent += ShowItemDescImage;
        _controller.OnHideItemDescEvent += HideItemDescImage;
        _closeInventoryButton.onClick.AddListener(_controller.CallOnClosePlayerInventoryEvent);
        base.CloseUI();
    }

    // �ڷ�ƾ �Լ��� ���� �������̵�
    public override void OpenUI()
    {
        base.OpenUI();
        StartCoroutine(SetBackgroundFillAmount(true));
    }

    public override void CloseUI()
    {
        StartCoroutine(SetBackgroundFillAmount(false));
    }

    // �κ��丮�� ���� ���� �� fill �ִϸ��̼��� �����ֱ� ���� �ڷ�ƾ �Լ�
    IEnumerator SetBackgroundFillAmount(bool isActive)
    {
        if (!isActive) SetChildObjectActive(isActive);

        float amount = _playerInventoryBackgroundImage.GetComponent<Image>().fillAmount;
        while (amount >= 0f && amount <= 1.0f)
        {
            if (isActive) amount += 0.1f;
            else amount -= 0.1f;
            _playerInventoryBackgroundImage.GetComponent<Image>().fillAmount = amount;
            yield return new WaitForFixedUpdate();
        }

        if (isActive) SetChildObjectActive(isActive);
        if (!isActive) base.CloseUI();
    }

    // �ڷ�ƾ �Լ� �����ϱ� ��, �Ŀ� �ڽ� ������Ʈ SetActive(true/false)
    private void SetChildObjectActive(bool isActive)
    {
        Transform transform = _playerInventoryBackgroundImage.transform;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isActive);
        }
    }

    public void SetInventoryUI(InventoryDTO inventory)
    {
        int idx = 0;
        foreach (ItemDTO item in inventory.ItemList)
        {
            GameObject container = _contentContainer.transform.GetChild(idx++).gameObject;
            container.SetActive(true);
            container.GetComponent<UIInventoryItemContainer>().SetItemInfo(item);
        }

        for (int i = idx; i < CONTAINER_NUM; i++)
        {
            GameObject container = _contentContainer.transform.GetChild(i).gameObject;
            container.SetActive(false);
        }
    }

    public void ShowItemDescImage(ItemDTO item, Vector3 position)
    {
        _itemNameText.text = item.Name;

        string effectStr = "";
        switch (item.Type)
        {
            case ItemType.HEALTH:
                effectStr = "�Ƿε� -";
                break;
            case ItemType.ATTACK:
                effectStr = "���ݷ� +";
                break;
            case ItemType.SHIELD:
                effectStr = "���� +";
                break;
        }
        _itemEffectText.text = effectStr + item.Value;
        _itemDescriptionImage.SetActive(true);

        position += new Vector3(-_itemDescriptionImage.GetComponent<RectTransform>().rect.width * 0.5f,
            -_itemDescriptionImage.GetComponent<RectTransform>().rect.height * 0.5f,
            0);
        _itemDescriptionImage.transform.position = position;

        _animator.SetBool("IsOpen", true);
    }

    public void HideItemDescImage()
    {
        _animator.SetBool("IsOpen", false);
        _itemDescriptionImage.SetActive(false);
    }
}
