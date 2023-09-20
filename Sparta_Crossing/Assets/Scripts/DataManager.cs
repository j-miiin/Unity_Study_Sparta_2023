using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    const string ITEM_DB_PATH = "DB/ItemDatabase";
    const string PLAYER_DB_PATH = "/Resources/DB/PlayerDatabase";

    public static DataManager Instance;

    private TextAsset _itemDatabase;

    public List<ItemDTO> itemList;

    public Dictionary<string, Sprite> itemDicspriteDic;

    private void Awake()
    {
        Instance = this;
        _itemDatabase = (TextAsset)Resources.Load(ITEM_DB_PATH);
        InitItemList();
        InitItemSprite();
    }

    // DB로부터 전체 아이템 리스트를 받아옴
    private void InitItemList()
    {
        string[] line = _itemDatabase.text.Substring(0, _itemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            itemList.Add(
                new ItemDTO(
                    row[0],
                    (ItemType)Enum.Parse(typeof(ItemType), row[1]), 
                    row[2],
                    int.Parse(row[3]),
                    int.Parse(row[4]),
                    row[5],
                    row[6] == "TRUE"
                ));
        }
    }

    private void InitItemSprite()
    {
        itemDicspriteDic = new Dictionary<string, Sprite>();
        foreach (ItemDTO item in itemList)
        {
            itemDicspriteDic.Add(item.Name, Resources.Load<Sprite>($"Images/{item.Image}"));
        }
    }

#nullable enable

    // DB에서 플레이어 정보를 읽어오고 없으면 null 리턴
    public PlayerDTO? GetPlayerInfo()
    {
        try
        {
            string jdata = File.ReadAllText(Application.dataPath + PLAYER_DB_PATH);
            Debug.Log(jdata);
            return JsonUtility.FromJson<PlayerDTO>(jdata);
        } catch
        {
            return null;
        }
    }

    // 플레이어 정보를 DB에 저장
    public void SavePlayerInfo(PlayerDTO player)
    {
        string jdata = JsonUtility.ToJson(player);
        File.WriteAllText(Application.dataPath + PLAYER_DB_PATH, jdata);
        Debug.Log("save " + jdata);
    }
}
