using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopUp : GameUIClass
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemDescriptionText;
    [SerializeField] private TMP_Text _itemEffectText;
    [SerializeField] private Button _positiveButton;
    [SerializeField] private Button _negativeButton;
    [SerializeField] private TMP_Text _positiveButtonText;

    private ItemDTO _item;

    private void Awake()
    {
        _positiveButton.onClick.AddListener(UseItem);
        _negativeButton.onClick.AddListener(CloseUI);
    }

    private void UseItem()
    {
        GameManager.Instance.UpdatePlayerStat(_item);
        CloseUI();
    }

    public void SetPopUp(ItemDTO item)
    {
        _item = item;

        _itemImage.sprite = DataManager.Instance.itemDicspriteDic[_item.Name];
        _itemNameText.text = _item.Name;
        _itemDescriptionText.text = _item.Description;

        string effectStr = "";
        switch (_item.Type)
        {
            // �Ҹ� ������
            case ItemType.HEALTH:
                effectStr = "�Ƿε� -";
                _positiveButtonText.text = "���";
                break;
            // ���ݿ� ���� ������
            case ItemType.ATTACK:
                effectStr = "���ݷ� +";
                _positiveButtonText.text = (_item.IsUsed) ? "����" : "����";
                break;
            // ���� ���� ������
            case ItemType.SHIELD:
                effectStr = "���� +";
                _positiveButtonText.text = (_item.IsUsed) ? "����" : "����";
                break;
        }

        _itemEffectText.text = effectStr + _item.Value;
    }
}
