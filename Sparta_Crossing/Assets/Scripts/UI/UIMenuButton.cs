using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuButton : GameUIClass
{
    [SerializeField] private Button _playerStatusButton;
    [SerializeField] private Button _playerInventoryButton;

    private UIController _controller;

    // �޴� ��ư�� Ŭ�� ������ ����
    void Start()
    {
        _controller = UIManager.Instance.controller;
        _controller.OnOpenPlayerInventoryEvent += CloseUI;
        _controller.OnClosePlayerInventoryEvent += OpenUI;
        _playerStatusButton.onClick.AddListener(_controller.CallOpenPlayerStatusEvent);
        _playerInventoryButton.onClick.AddListener(_controller.CallOpenPlayerInventoryEvent);
    }

    // �÷��̾� �κ��丮 ����
    private void OpenPlayerInventory()
    {
        GameUIClass uiPlayerInventory = UIManager.Instance.GetUIComponent<UIPlayerInventory>();
        uiPlayerInventory.OpenUI();
    }
}
