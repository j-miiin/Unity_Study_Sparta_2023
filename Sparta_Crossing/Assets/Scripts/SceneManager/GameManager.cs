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

    // �÷��̾� ������ DB�κ��� �о��
    // �÷��̾� ������ ������ ���ο� �÷��̾� ����
    private void InitPlayer()
    {
        PlayerDTO tmpPlayer = DataManager.Instance.GetPlayerInfo();

        if (tmpPlayer == null)
        {
            player = new PlayerDTO("���� ĳ�� ���ڵ���");
        } else
        {
            player = tmpPlayer;
        }
    }

    // ����� UI ������Ʈ���� UI Manager�κ��� �޾ƿ�
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