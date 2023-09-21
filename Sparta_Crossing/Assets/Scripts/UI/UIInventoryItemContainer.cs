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
    [SerializeField] private GameObject _itemDescriptionImage;
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemEffectText;

    private Animator _animator;

    private ItemDTO _item;

    // 인벤토리에서 아이템을 클릭하면 팝업이 나타나도록 클릭 리스너 연결
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OpenItemPopUp);
        _animator = _itemDescriptionImage.GetComponent<Animator>();
    }

    private void OpenItemPopUp()
    {
        UIPopUp uiPopUp = UIManager.Instance.GetUIComponent<UIPopUp>();
        uiPopUp.OpenUI();
        uiPopUp.SetPopUp(_item);
    }

    // 각각의 아이템 정보에 따라서 UI를 set
    public void SetItemInfo(ItemDTO item)
    {
        _item = item;

        _inventoryItemImage.sprite = DataManager.Instance.itemDicspriteDic[item.Name];

        switch (_item.Type)
        {
            // 소모 아이템일 경우 항상 개수 표시
            case ItemType.HEALTH:
                _inventoryItemStatusImage.SetActive(true);
                SetInventoryItemStatusText(true, _item.Count);
                break;
            // 장착 아이템일 경우 장착 상태일 때만 장착 표시
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

    // 장착 아이템은 장착 상태일 경우 "E" 표시
    // 소모 아이템은 개수 표시 (10개 넘으면 10+로 표시)
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

    public void OnPointerEnter()
    {
        _itemNameText.text = _item.Name;

        string effectStr = "";
        switch (_item.Type)
        {
            case ItemType.HEALTH:
                effectStr = "피로도 -";
                break;
            case ItemType.ATTACK:
                effectStr = "공격력 +";
                break;
            case ItemType.SHIELD:
                effectStr = "방어력 +";
                break;
        }
        _itemEffectText.text = effectStr + _item.Value;
        _itemDescriptionImage.SetActive(true);
        _animator.SetBool("IsOpen", true);
    }

    public void OnPointerExit()
    {
        _animator.SetBool("IsOpen", false);
        _itemDescriptionImage.SetActive(false);
    }
}
