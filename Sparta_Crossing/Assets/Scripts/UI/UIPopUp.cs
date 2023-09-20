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
            // 소모 아이템
            case ItemType.HEALTH:
                effectStr = "피로도 -";
                _positiveButtonText.text = "사용";
                break;
            // 공격용 장착 아이템
            case ItemType.ATTACK:
                effectStr = "공격력 +";
                _positiveButtonText.text = (_item.IsUsed) ? "해제" : "장착";
                break;
            // 방어용 장착 아이템
            case ItemType.SHIELD:
                effectStr = "방어력 +";
                _positiveButtonText.text = (_item.IsUsed) ? "해제" : "장착";
                break;
        }

        _itemEffectText.text = effectStr + _item.Value;
    }
}
