using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = new Player("감자돌이");
        UIPlayerInfo uiPlayerInfo = UIManager.Instance.GetUIComponent<UIPlayerInfo>();
        uiPlayerInfo.SetPlayerInfo(player);

        UIPlayerInventory uiPlayerInventory = UIManager.Instance.GetUIComponent<UIPlayerInventory>();
        uiPlayerInventory.SetInventoryUI(player.PlayerInventory);
    }

    private void Update()
    {
        
    }
}