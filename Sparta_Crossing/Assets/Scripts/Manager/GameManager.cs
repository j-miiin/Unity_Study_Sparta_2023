using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private SceneStateType _curState;

    private PlayerDTO player;

    private UITitle _uiTitle;
    private UIMenuButton _uiMenuButton;
    private UIPlayerInfo _uiPlayerInfo;
    private UIPlayerStatus _uiPlayerStatus;
    private UIPlayerInventory _uiPlayerInventory;
    private UIStore _uiStore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        _curState = SceneStateType.START;

        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void Start()
    {
        InitUIComponent();
    }

    public void OnActiveSceneChanged(Scene scene1, Scene scene2)
    {
        InitUIComponent();
    }

    // 플레이어 정보를 DB로부터 읽어옴
    // 플레이어 정보가 없으면 새로운 플레이어 생성
    private void InitPlayer()
    {
        PlayerDTO tmpPlayer = DataManager.Instance.GetPlayerInfo();

        if (tmpPlayer == null)
        {
            player = new PlayerDTO(PlayerPrefs.GetString(PrefsKey.PLAYER_NAME));
        } else
        {
            player = tmpPlayer;
        }
        // 인벤토리에 아이템 넣어서 테스트할 때 주석 풀기
        SetDefaultInventory();
    }

    // 초기 아이템 넣어주기 위한 함수 (테스트용)
    private void SetDefaultInventory()
    {
        for (int i = 0; i < DataManager.Instance.itemList.Count; i++)
        {
            ItemDTO item = DataManager.Instance.itemList[i];
            player.Inventory.AddItem(item);
            if (i == 0)
            {
                for (int j = 0; j < 15; j++) player.Inventory.AddItem(item);
            }
            if (i == 3)
            {
                for (int j = 0; j < 5; j++) player.Inventory.AddItem(item);
            }
        }
    }
   
    private void InitUIComponent()
    {
        switch (_curState)
        {
            case SceneStateType.START:
                InitStartSceneComponent();
                break;
            case SceneStateType.MAIN:
                InitMainSceneComponent();
                break;
        }        
    }

    // 사용할 UI 컴포넌트들을 UI Manager로부터 받아옴
    private void InitStartSceneComponent()
    {
        _uiTitle = UIManager.Instance.GetUIComponent<UITitle>();
    }

    private void InitMainSceneComponent()
    {
        _uiMenuButton = UIManager.Instance.GetUIComponent<UIMenuButton>();

        _uiPlayerInfo = UIManager.Instance.GetUIComponent<UIPlayerInfo>();
        _uiPlayerInfo.SetPlayerInfo(player);

        _uiPlayerStatus = UIManager.Instance.GetUIComponent<UIPlayerStatus>();
        _uiPlayerStatus.SetPlayerStat(player);

        _uiPlayerInventory = UIManager.Instance.GetUIComponent<UIPlayerInventory>();
        _uiPlayerInventory.SetInventoryUI(player.Inventory);

        _uiStore = UIManager.Instance.GetUIComponent<UIStore>();
    }

    // StartScene -> MainScene으로 넘어갈 때, StartScene의 UI 컴포넌트들을 UI Manager의 Dictionary에서 삭제
    // 현재 Scene 이동 구간은 StartScene -> MainScene 밖에 없어서 StartScene의 UI 컴포넌트를 삭제하는 메서드만 있음
    private void RemoveStartSceneComponent()
    {
        UIManager.Instance.RemoveUIComponent(_uiTitle);
    }

    public void LoadMainScene(string playerName)
    {
        // 데이터 저장
        if (playerName.Length != 0) PlayerPrefs.SetString(PrefsKey.PLAYER_NAME, playerName);
        InitPlayer();
        DataManager.Instance.SavePlayerInfo(player);

        // UI 변경
        _curState = SceneStateType.MAIN;
        RemoveStartSceneComponent();

        // Scene 이동
        SceneManager.LoadScene("MainScene");
    }

    public void UpdatePlayerStat(ItemDTO item)
    {
        player.UseItem(item);
        _uiPlayerInventory.SetInventoryUI(player.Inventory);
        _uiPlayerStatus.SetPlayerStat(player);
        DataManager.Instance.SavePlayerInfo(player);
    }
}