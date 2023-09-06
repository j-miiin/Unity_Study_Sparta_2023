using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 플레이어 닉네임
    private Text playerNameText;

    // 참가자 이름 리스트 컨테이너
    [SerializeField] private GameObject contentContainer;

    // Panel
    private GameObject attendeeListPanel;
    private GameObject selectPlayerCharacterPanel;
    private GameObject updatePlayerNamePanel;
    private InputField playerNameInputField;

    // Bottom Bar
    private Button attendeeListBtn;
    private Button closeAttendeeListBtn;
    private Button openPlayerCharacterPanelBtn;
    private Button openUpdatePlayerNamePanelBtn;
    private Button updatePlayerNameBtn;
    private Button characterOneSelectBtn;
    private Button characterTwoSelectBtn;

    // 시간 표시
    private Text clockText;

    // NPC 호출
    private GameObject callNpcPanel;
    private Text calledNpcNameText;
    private Button callNpcBtn;
    
    // NPC 대화
    private GameObject npcInteractionPanel;
    private Text interactNpcNameText;
    private Text npcDialogText;
    private Button nextDialogBtn;

    const string PLAYER = "Player";
    const string CANVAS = "Canvas";
    const string PLAYER_NAME_TEXT = "PlayerNameText";
    const string ATTENDEE_TEXT_CONTAINER = "AttendeeListPanel/AttendeeListScrollView/Viewport/Content";
    const string ATTENDEE_LIST_PANEL = "AttendeeListPanel";
    const string SELECT_PLAYER_CHARACTER_PANEL = "SelectPlayerCharacterPanel";
    const string UPDATE_PLAYER_NAME_PANEL = "UpdatePlayerNamePanel";
    const string PLAYER_NAME_INPUT_FIELD = "PlayerNameInputField";
    const string ATTENDEE_LIST_BTN = "BottomBar/AttendeeListBtn";
    const string CLOSE_ATTENDEE_LIST_BTN = "AttendeeListPanel/AttendeeListCloseBtn";
    const string OPEN_PLAYER_CHARACTER_PANEL_BTN = "BottomBar/OpenPlayerCharacterPanelBtn";
    const string OPEN_UPDATE_PLAYER_NAME_PANEL_BTN = "BottomBar/OpenUpdatePlayerNamePanelBtn";
    const string UPDATE_PLAYER_NAME_BTN = "UpdatePlayerNameBtn";
    const string CHARACTER_ONE_SELECT_BTN = "Character1";
    const string CHARACTER_TWO_SELECT_BTN = "Character2";
    const string CLOCK_TEXT = "ClockText";
    const string CALL_NPC_PANEL = "CallNpcPanel";
    const string CALLED_NPC_NAME_TEXT = "CalledNpcNameText";
    const string CALL_NPC_BTN = "CallNpcBtn";
    const string NPC_INTERACTION_PANEL = "NpcInteractionPanel";
    const string INTERACT_NPC_NAME_TEXT = "InteractNpcNameText";
    const string NPC_DIALOG_TEXT = "NpcDialogText";
    const string NEXT_DIALOG_BTN = "NextDialogBtn";

    private GameObject playerObject;
    private GameObject canvas;

    public static UIManager U;

    private void Awake()
    {
        U = this;
        playerObject = GameObject.FindWithTag(PLAYER).gameObject;
    }

    private void Start()
    {
        InitUIObject();
        AddListener();
    }

    private void Update()
    {
        string time = DateTime.Now.ToString("t");
        clockText.text = time;
        playerNameText.transform.position = Camera.main.WorldToScreenPoint(playerObject.transform.position + new Vector3(0, 1.2f, 0));
    }

    private void InitUIObject()
    {
        // 플레이어 닉네임
        canvas = GameObject.FindWithTag(CANVAS);
        playerNameText = canvas.transform.Find(PLAYER_NAME_TEXT).GetComponent<Text>();
        playerNameText.text = playerObject.GetComponent<Player>().Name;

        // 참가자 이름 리스트 컨테이너
        contentContainer = canvas.transform.Find(ATTENDEE_TEXT_CONTAINER).gameObject;

        // Panel
        attendeeListPanel = canvas.transform.Find(ATTENDEE_LIST_PANEL).gameObject;
        selectPlayerCharacterPanel = canvas.transform.Find(SELECT_PLAYER_CHARACTER_PANEL).gameObject;
        updatePlayerNamePanel = canvas.transform.Find(UPDATE_PLAYER_NAME_PANEL).gameObject;
        playerNameInputField = canvas.transform.Find($"{UPDATE_PLAYER_NAME_PANEL}/{PLAYER_NAME_INPUT_FIELD}").GetComponent<InputField>();

        // Bottom Bar
        attendeeListBtn = canvas.transform.Find(ATTENDEE_LIST_BTN).GetComponent<Button>();
        closeAttendeeListBtn = canvas.transform.Find(CLOSE_ATTENDEE_LIST_BTN).GetComponent<Button>();
        openPlayerCharacterPanelBtn = canvas.transform.Find(OPEN_PLAYER_CHARACTER_PANEL_BTN).GetComponent<Button>();
        openUpdatePlayerNamePanelBtn = canvas.transform.Find(OPEN_UPDATE_PLAYER_NAME_PANEL_BTN).GetComponent<Button>();
        updatePlayerNameBtn = canvas.transform.Find($"{UPDATE_PLAYER_NAME_PANEL}/{UPDATE_PLAYER_NAME_BTN}").GetComponent<Button>();
        characterOneSelectBtn = canvas.transform.Find($"{SELECT_PLAYER_CHARACTER_PANEL}/{CHARACTER_ONE_SELECT_BTN}").GetComponent<Button>();
        characterTwoSelectBtn = canvas.transform.Find($"{SELECT_PLAYER_CHARACTER_PANEL}/{CHARACTER_TWO_SELECT_BTN}").GetComponent<Button>();

        // 시간 표시
        clockText = canvas.transform.Find(CLOCK_TEXT).GetComponent<Text>();

        // NPC 호출
        callNpcPanel = canvas.transform.Find(CALL_NPC_PANEL).gameObject;
        calledNpcNameText = canvas.transform.Find($"{CALL_NPC_PANEL}/{CALLED_NPC_NAME_TEXT}").GetComponent<Text>();
        callNpcBtn = canvas.transform.Find($"{CALL_NPC_PANEL}/{CALL_NPC_BTN}").GetComponent<Button>();

        // NPC 대화
        npcInteractionPanel = canvas.transform.Find(NPC_INTERACTION_PANEL).gameObject;
        interactNpcNameText = canvas.transform.Find($"{NPC_INTERACTION_PANEL}/{INTERACT_NPC_NAME_TEXT}").GetComponent<Text>();
        npcDialogText = canvas.transform.Find($"{NPC_INTERACTION_PANEL}/{NPC_DIALOG_TEXT}").GetComponent<Text>();
        nextDialogBtn = canvas.transform.Find($"{NPC_INTERACTION_PANEL}/{NEXT_DIALOG_BTN}").GetComponent<Button>();
    }

    private void AddListener()
    {
        attendeeListBtn.onClick.AddListener(OpenAttendeeList);
        closeAttendeeListBtn.onClick.AddListener(CloseAttendeeList);
        openPlayerCharacterPanelBtn.onClick.AddListener(OpenPlayerCharacterPanel);
        openUpdatePlayerNamePanelBtn.onClick.AddListener(OpenUpdatePlayerNamePanel);
        updatePlayerNameBtn.onClick.AddListener(UpdatePlayerName);
        characterOneSelectBtn.onClick.AddListener(SelectCharacter1);
        characterTwoSelectBtn.onClick.AddListener(SelectCharacter2);
        callNpcBtn.onClick.AddListener(CallNpc);
        nextDialogBtn.onClick.AddListener(CloseNpcDialogPanel);
    }

    public void SetAttendeeList()
    {
        // 최상단에 플레이어 닉네임
        GameObject playerName = Instantiate(Resources.Load<GameObject>("Prefabs/AttendeeName"));
        playerName.GetComponent<Text>().text = playerObject.GetComponent<Player>().Name;
        playerName.transform.SetParent(contentContainer.transform);

        // Npc 닉네임
        GameObject npcController = GameObject.Find("NpcController");
        int npcNum = npcController.transform.childCount;

        for (int i = 0; i < npcNum; i++)
        {
            GameObject npcName = Instantiate(Resources.Load<GameObject>("Prefabs/AttendeeName"));
            npcName.GetComponent<Text>().text = npcController.transform.GetChild(i).GetComponent<Npc>().Name;
            npcName.transform.SetParent(contentContainer.transform);
        }
    }

    public void OpenAttendeeList()
    {
        attendeeListPanel.SetActive(true);
    }

    public void CloseAttendeeList()
    {
        attendeeListPanel.SetActive(false);
    }

    public void OpenPlayerCharacterPanel()
    {
        selectPlayerCharacterPanel.SetActive(true);
    }

    public void OpenUpdatePlayerNamePanel()
    {
        updatePlayerNamePanel.SetActive(true);
    }

    public void UpdatePlayerName()
    {
        string playerName = playerNameInputField.text;
        if (playerName.Length > 0)
        {
            playerNameText.text = playerName;
            playerObject.GetComponent<Player>().SetPlayerName(playerName);
            playerNameInputField.text = "";
            updatePlayerNamePanel.SetActive(false);
        }
    }

    public void SelectCharacter1()
    {
        playerObject.GetComponent<Player>().SetPlayerCharacter(PlayerCharacterType.ONE);
        selectPlayerCharacterPanel.SetActive(false);
    }

    public void SelectCharacter2()
    {
        playerObject.GetComponent<Player>().SetPlayerCharacter(PlayerCharacterType.TWO);
        selectPlayerCharacterPanel.SetActive(false);
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
