using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> ItemList { get; set; }
 
    public Inventory()
    {
        ItemList = new List<Item>();
        InitInventory();
    }

    private void InitInventory()
    {
        ItemList.Add(new Item(
            "µµ³Ó",
            ItemType.SHIELD,
            "",
            "donnut_stroke"));
    }
}
