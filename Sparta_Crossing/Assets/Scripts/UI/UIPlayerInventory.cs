using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIPlayerInventory : GameUIClass
{
    const int CONTAINER_NUM = 20;

    [SerializeField] private GameObject _playerInventoryBackgroundImage;
    [SerializeField] private Button _closeInventoryButton;
    [SerializeField] private GameObject _contentContainer;

    // �κ��丮 �ݱ� ��ư�� Ŭ�� ������ ����
    private void Awake()
    {
        transform.localScale = Vector3.one;
        _closeInventoryButton.onClick.AddListener(CloseUI);
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
            Debug.Log(_contentContainer.transform.childCount);
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
}
