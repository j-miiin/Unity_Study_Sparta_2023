using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }

    public Item(string name, ItemType type, string description, string image)
    {
        Name = name;
        Type = type;
        Description = description;
        Image = image;
    }
}
