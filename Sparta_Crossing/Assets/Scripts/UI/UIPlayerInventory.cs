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

    // 인벤토리 닫기 버튼에 클릭 리스너 연결
    private void Awake()
    {
        transform.localScale = Vector3.one;
        _closeInventoryButton.onClick.AddListener(CloseUI);
    }

    // 코루틴 함수를 위해 오버라이드
    public override void OpenUI()
    {
        base.OpenUI();
        StartCoroutine(SetBackgroundFillAmount(true));
    }

    public override void CloseUI()
    {
        StartCoroutine(SetBackgroundFillAmount(false));
    }

    // 인벤토리를 열고 닫을 때 fill 애니메이션을 보여주기 위한 코루틴 함수
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

    // 코루틴 함수 실행하기 전, 후에 자식 오브젝트 SetActive(true/false)
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
