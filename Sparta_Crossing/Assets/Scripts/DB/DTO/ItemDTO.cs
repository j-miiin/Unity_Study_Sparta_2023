using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemDTO 
{
    public string Name;
    public ItemType Type;
    public string Description;
    public string Image;
    public bool IsUsed;

    public ItemDTO(string name, ItemType type, string description, string image, bool isUsed)
    {
        Name = name;
        Type = type;
        Description = description;
        Image = image;
        IsUsed = isUsed;
    }
}
