using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInventory : GameUIClass
{
    [SerializeField] private Button _closeInventoryButton;
    [SerializeField] private GameObject _contentContainer;

    private void Awake()
    {
        _closeInventoryButton.onClick.AddListener(CloseUI);
    }

    public void SetInventoryUI(InventoryDTO inventory)
    {
        foreach (ItemDTO item in inventory.ItemList)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/InventoryItemContainer"));
            Image itemImage = obj.transform.GetChild(0).GetComponent<Image>();
            itemImage.sprite = Resources.Load<Sprite>($"Images/{item.Image}");
            obj.transform.SetParent(_contentContainer.transform);
        }
    }
}
