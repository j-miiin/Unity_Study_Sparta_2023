using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuButton : GameUIClass
{
    [SerializeField] private Button _playerStatusButton;
    [SerializeField] private Button _playerInventoryButton;

    // 메뉴 버튼에 클릭 리스너 연결
    private void Start()
    {
        _playerStatusButton.onClick.AddListener(OpenPlayerStatus);
        _playerInventoryButton.onClick.AddListener(OpenPlayerInventory);
    }

    // 플레이어 상태창 열기
    private void OpenPlayerStatus()
    {
        GameUIClass uiPlayerStatus = UIManager.Instance.GetUIComponent<UIPlayerStatus>();
        uiPlayerStatus.OpenUI();
    }

    // 플레이어 인벤토리 열기
    private void OpenPlayerInventory()
    {
        GameUIClass uiPlayerInventory = UIManager.Instance.GetUIComponent<UIPlayerInventory>();
        uiPlayerInventory.OpenUI();
    }
}
