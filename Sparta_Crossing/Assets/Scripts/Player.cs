using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int Gold { get; set; }

    public Inventory PlayerInventory { get; set; }

    public Player(string name)
    {
        Name = name;
        Level = 1;
        Gold = 0;
        PlayerInventory = new Inventory();
    }
}
