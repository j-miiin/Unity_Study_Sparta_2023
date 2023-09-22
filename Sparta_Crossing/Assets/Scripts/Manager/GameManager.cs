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

    // �÷��̾� ������ DB�κ��� �о��
    // �÷��̾� ������ ������ ���ο� �÷��̾� ����
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
        // �κ��丮�� ������ �־ �׽�Ʈ�� �� �ּ� Ǯ��
        SetDefaultInventory();
    }

    // �ʱ� ������ �־��ֱ� ���� �Լ� (�׽�Ʈ��)
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

    // ����� UI ������Ʈ���� UI Manager�κ��� �޾ƿ�
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

    // StartScene -> MainScene���� �Ѿ ��, StartScene�� UI ������Ʈ���� UI Manager�� Dictionary���� ����
    // ���� Scene �̵� ������ StartScene -> MainScene �ۿ� ��� StartScene�� UI ������Ʈ�� �����ϴ� �޼��常 ����
    private void RemoveStartSceneComponent()
    {
        UIManager.Instance.RemoveUIComponent(_uiTitle);
    }

    public void LoadMainScene(string playerName)
    {
        // ������ ����
        if (playerName.Length != 0) PlayerPrefs.SetString(PrefsKey.PLAYER_NAME, playerName);
        InitPlayer();
        DataManager.Instance.SavePlayerInfo(player);

        // UI ����
        _curState = SceneStateType.MAIN;
        RemoveStartSceneComponent();

        // Scene �̵�
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