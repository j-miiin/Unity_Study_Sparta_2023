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
    public int Value;
    public int Count;
    public bool IsUsed;
    public string Image;

    public ItemDTO(string name, ItemType type, string description, int value, int count, string image, bool isUsed)
    {
        Name = name;
        Type = type;
        Description = description;
        Value = value;
        Count = count;
        Image = image;
        IsUsed = isUsed;
    }
}
