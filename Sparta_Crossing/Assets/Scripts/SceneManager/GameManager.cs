using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private PlayerDTO player;

    private UIMenuButton _uiMenuButton;
    private UIPlayerInfo _uiPlayerInfo;
    private UIPlayerStatus _uiPlayerStatus;
    private UIPlayerInventory _uiPlayerInventory;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitPlayer();

        //for (int i = 0; i < 5; i++)
        //{
        //    ItemDTO consumableItem = DataManager.Instance.itemList[0];
        //    player.Inventory.AddItem(consumableItem);
        //}

        //ItemDTO equipableItem1 = DataManager.Instance.itemList[1];
        //equipableItem1.IsUsed = true;
        //player.Inventory.AddItem(equipableItem1);

        //ItemDTO equipableItem2 = DataManager.Instance.itemList[2];
        //player.Inventory.AddItem(equipableItem2);

        DataManager.Instance.SavePlayerInfo(player);

        InitUIComponent();
    }

    // 플레이어 정보를 DB로부터 읽어옴
    // 플레이어 정보가 없으면 새로운 플레이어 생성
    private void InitPlayer()
    {
        PlayerDTO tmpPlayer = DataManager.Instance.GetPlayerInfo();

        if (tmpPlayer == null)
        {
            player = new PlayerDTO("감자 캐는 감자돌이");
        } else
        {
            player = tmpPlayer;
        }
    }

    // 사용할 UI 컴포넌트들을 UI Manager로부터 받아옴
    private void InitUIComponent()
    {
        _uiMenuButton = UIManager.Instance.GetUIComponent<UIMenuButton>();

        _uiPlayerInfo = UIManager.Instance.GetUIComponent<UIPlayerInfo>();
        _uiPlayerInfo.SetPlayerInfo(player);

        _uiPlayerStatus = UIManager.Instance.GetUIComponent<UIPlayerStatus>();
        _uiPlayerStatus.SetPlayerStat(player);

        _uiPlayerInventory = UIManager.Instance.GetUIComponent<UIPlayerInventory>();
        _uiPlayerInventory.SetInventoryUI(player.Inventory);
    }

    public void UpdatePlayerStat(ItemDTO item)
    {
        player.UseItem(item);
        _uiPlayerInventory.SetInventoryUI(player.Inventory);
        _uiPlayerStatus.SetPlayerStat(player);
        DataManager.Instance.SavePlayerInfo(player);
    }
}