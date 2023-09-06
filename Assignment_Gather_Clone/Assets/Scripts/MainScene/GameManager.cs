using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    private void Awake()
    {
        I = this;
    }

    void Start()
    {
        LoadAttendeeList();
    }

    void Update()
    {
        
    }

    private void LoadAttendeeList()
    {
        GameObject npcController = GameObject.Find("NpcController");
        GameObject[] npcList = Resources.LoadAll<GameObject>("Prefabs/Npc");
        foreach (GameObject npc in npcList)
        {
            GameObject curNpc = Instantiate(npc);
            curNpc.transform.SetParent(npcController.transform);
        }

        UIManager.U.SetAttendeeList();
    }
}
