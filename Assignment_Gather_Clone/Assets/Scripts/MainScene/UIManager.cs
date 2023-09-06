using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 참가자 이름 리스트 컨테이너
    [SerializeField] private Transform contentContainer;

    // 시간 표시
    [SerializeField] private Text clockText;

    // NPC 호출
    [SerializeField] private GameObject callNpcPanel;
    [SerializeField] private Text calledNpcNameText;
    [SerializeField] private Button callNpcBtn;
    
    // NPC 대화
    [SerializeField] private GameObject npcInteractionPanel;
    [SerializeField] private Text interactNpcNameText;
    [SerializeField] private Text npcDialogText;
    [SerializeField] private Button nextDialogBtn;

    public static UIManager U;

    private void Awake()
    {
        U = this;
    }

    private void Start()
    {
        callNpcBtn.onClick.AddListener(CallNpc);
        nextDialogBtn.onClick.AddListener(CloseNpcDialogPanel);
    }

    private void Update()
    {
        string time = DateTime.Now.ToString("t");
        clockText.text = time;
    }

    public void SetAttendeeList()
    {
        // 최상단에 플레이어 닉네임
        GameObject playerName = Instantiate(Resources.Load<GameObject>("Prefabs/AttendeeName"));
        playerName.GetComponent<Text>().text = PlayerPrefs.GetString(Player.PLAYER_NAME);
        playerName.transform.SetParent(contentContainer);

        // Npc 닉네임
        GameObject npcController = GameObject.Find("NpcController");
        int npcNum = npcController.transform.childCount;

        for (int i = 0; i < npcNum; i++)
        {
            GameObject npcName = Instantiate(Resources.Load<GameObject>("Prefabs/AttendeeName"));
            npcName.GetComponent<Text>().text = npcController.transform.GetChild(i).GetComponent<Npc>().Name;
            npcName.transform.SetParent(contentContainer);
        }
    }

    public void SetNpcDialogPanel(string npcName, string npcDialog)
    {
        calledNpcNameText.text = npcName;
        interactNpcNameText.text = npcName;
        npcDialogText.text = npcDialog;
    }

    public void CallNpc()
    {
        callNpcPanel.SetActive(false);
        npcInteractionPanel.SetActive(true);
    }

    public void CloseNpcDialogPanel()
    {
        npcInteractionPanel.SetActive(false);
    }
}
