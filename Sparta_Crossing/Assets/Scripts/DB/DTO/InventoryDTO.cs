using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryDTO
{
    public List<ItemDTO> ItemList;
 
    public InventoryDTO()
    {
        ItemList = new List<ItemDTO>();
    }

    public void AddItem(ItemDTO item)
    {
        ItemList.Add(item);
    }

    public ItemDTO GetItem(int index)
    {
        return ItemList[index];
    }
}
