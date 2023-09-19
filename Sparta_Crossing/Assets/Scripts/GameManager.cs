using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private PlayerDTO player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitPlayer();

        //ItemDTO newItem = DataManager.Instance.itemList[0];
        //player.Inventory.AddItem(newItem);

        DataManager.Instance.SavePlayerInfo(player);
        UIPlayerInfo uiPlayerInfo = UIManager.Instance.GetUIComponent<UIPlayerInfo>();
        uiPlayerInfo.SetPlayerInfo(player);

        UIPlayerInventory uiPlayerInventory = UIManager.Instance.GetUIComponent<UIPlayerInventory>();
        uiPlayerInventory.SetInventoryUI(player.Inventory);
    }

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
}