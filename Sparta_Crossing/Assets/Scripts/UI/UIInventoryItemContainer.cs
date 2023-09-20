using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItemContainer : GameUIClass
{
    const int LARGE_FONT_SIZE = 35;
    const int SMALL_FONT_SIZE = 25;

    [SerializeField] private Image _inventoryItemImage;
    [SerializeField] private GameObject _inventoryItemStatusImage;
    [SerializeField] private TMP_Text _inventoryItemStatusText;

    private ItemDTO _item;

    // �κ��丮���� �������� Ŭ���ϸ� �˾��� ��Ÿ������ Ŭ�� ������ ����
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OpenItemPopUp);
    }

    private void OpenItemPopUp()
    {
        UIPopUp uiPopUp = UIManager.Instance.GetUIComponent<UIPopUp>();
        uiPopUp.OpenUI();
        uiPopUp.SetPopUp(_item);
    }

    // ������ ������ ������ ���� UI�� set
    public void SetItemInfo(ItemDTO item)
    {
        _item = item;

        _inventoryItemImage.sprite = DataManager.Instance.itemDicspriteDic[item.Name];

        switch (_item.Type)
        {
            // �Ҹ� �������� ��� �׻� ���� ǥ��
            case ItemType.HEALTH:
                _inventoryItemStatusImage.SetActive(true);
                SetInventoryItemStatusText(true, _item.Count);
                break;
            // ���� �������� ��� ���� ������ ���� ���� ǥ��
            case ItemType.ATTACK:
            case ItemType.SHIELD:
                if (_item.IsUsed)
                {
                    _inventoryItemStatusImage.SetActive(true);
                    SetInventoryItemStatusText(false, 0);
                }
                else
                {
                    _inventoryItemStatusImage.SetActive(false);
                }
                break;
        }
    }

    // ���� �������� ���� ������ ��� "E" ǥ��
    // �Ҹ� �������� ���� ǥ�� (10�� ������ 10+�� ǥ��)
    private void SetInventoryItemStatusText(bool isConsumable, int itemNum)
    {
        if (isConsumable)
        {
            _inventoryItemStatusText.text = (itemNum <= 10) ? itemNum.ToString() : "10+";
            _inventoryItemStatusText.fontSize = (itemNum <= 10) ? LARGE_FONT_SIZE : SMALL_FONT_SIZE;
        } else
        {
            _inventoryItemStatusText.text = "E";
            _inventoryItemStatusText.fontSize = LARGE_FONT_SIZE;
        }
    }
}
