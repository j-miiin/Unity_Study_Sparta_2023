using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void SetInventoryUI(Inventory inventory)
    {
        foreach (Item item in inventory.ItemList)
        {
            var obj = Instantiate(Resources.Load("Prefabs/InventoryItemContainer"));
            ((GameObject)obj).transform.SetParent(_contentContainer.transform);
            Image itemImage = obj.GetComponentInChildren<Image>();
            itemImage.sprite = Resources.Load($"Images/{item.Image}").GetComponent<Image>().sprite;
        }
    }
}
