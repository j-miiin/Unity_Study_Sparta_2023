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
        SetDefaultInventory();
    }

    private void SetDefaultInventory()
    {
        for (int i = 0; i < 5; i++)
        {
            ItemDTO consumableItem = DataManager.Instance.itemList[0];
            player.Inventory.AddItem(consumableItem);
        }

        ItemDTO equipableItem1 = DataManager.Instance.itemList[1];
        player.Inventory.AddItem(equipableItem1);

        ItemDTO equipableItem2 = DataManager.Instance.itemList[2];
        player.Inventory.AddItem(equipableItem2);
    }

    // ����� UI ������Ʈ���� UI Manager�κ��� �޾ƿ�
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
    }

    private void DestroyStartSceneComponent()
    {
        UIManager.Instance.RemoveUIComponent(_uiTitle);
    }

    public void LoadMainScene(string playerName)
    {
        // Scene �̵�
        SceneManager.LoadScene("MainScene");

        // ������ ����
        if (playerName.Length != 0) PlayerPrefs.SetString(PrefsKey.PLAYER_NAME, playerName);
        InitPlayer();
        DataManager.Instance.SavePlayerInfo(player);

        // UI ����
        _curState = SceneStateType.MAIN;
        DestroyStartSceneComponent();
    }

    public void UpdatePlayerStat(ItemDTO item)
    {
        player.UseItem(item);
        _uiPlayerInventory.SetInventoryUI(player.Inventory);
        _uiPlayerStatus.SetPlayerStat(player);
        DataManager.Instance.SavePlayerInfo(player);
    }
}