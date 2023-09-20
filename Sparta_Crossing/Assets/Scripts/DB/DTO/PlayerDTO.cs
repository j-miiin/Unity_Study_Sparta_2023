using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[Serializable]
public class PlayerDTO
{
    public string Name;
    public int Level;
    public int Gold;
    public int Health;
    public int Attack;
    public int Shield;
    public int Tiredness;
    public InventoryDTO Inventory;

    public PlayerDTO(string name)
    {
        Name = name;
        Level = 1;
        Gold = 20000;
        Health = 100;
        Attack = 10;
        Shield = 10;
        Tiredness = 0;
        Inventory = new InventoryDTO();
    }

    public void UseItem(ItemDTO item)
    {
        switch (item.Type)
        {
            // 소모 아이템
            case ItemType.HEALTH:
                Tiredness -= item.Value;
                Tiredness = Mathf.Max(0, Tiredness);
                item.Count--;
                if (item.Count == 0) Inventory.ItemList.Remove(item);
                break;
            // 공격용 장착 아이템
            case ItemType.ATTACK:
                if (!item.IsUsed)
                {
                    ItemDTO tmpAttackItem = Inventory.ItemList.Find(x => (x.Type == ItemType.ATTACK) && x.IsUsed);
                    if (tmpAttackItem != null)
                    {
                        Attack -= tmpAttackItem.Value;
                        tmpAttackItem.IsUsed = false;
                    }
                    Attack += item.Value;
                    item.IsUsed = true;
                } else
                {
                    Attack -= item.Value;
                    item.IsUsed = false;
                }
                break;
            // 방어용 장착 아이템
            case ItemType.SHIELD:
                if (!item.IsUsed)
                {
                    ItemDTO tmpShieldItem = Inventory.ItemList.Find(x => (x.Type == ItemType.SHIELD) && x.IsUsed);
                    if (tmpShieldItem != null)
                    {
                        Shield -= tmpShieldItem.Value;
                        tmpShieldItem.IsUsed = false;
                    }
                    Shield += item.Value;
                    item.IsUsed = true;
                }
                else
                {
                    Shield -= item.Value;
                    item.IsUsed = false;
                }
                break;
        }
    }
}
