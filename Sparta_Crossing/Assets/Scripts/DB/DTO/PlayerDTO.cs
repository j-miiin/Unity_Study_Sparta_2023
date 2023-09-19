using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDTO
{
    public string Name;
    public int Level;
    public int Gold;
    public InventoryDTO Inventory;

    public PlayerDTO(string name)
    {
        Name = name;
        Level = 1;
        Gold = 0;
        Inventory = new InventoryDTO();
    }
}
