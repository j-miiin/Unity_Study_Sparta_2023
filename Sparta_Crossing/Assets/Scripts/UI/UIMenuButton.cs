using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuButton : GameUIClass
{
    [SerializeField] private Button _playerStatusButton;
    [SerializeField] private Button _playerInventoryButton;

    // �޴� ��ư�� Ŭ�� ������ ����
    private void Start()
    {
        _playerStatusButton.onClick.AddListener(OpenPlayerStatus);
        _playerInventoryButton.onClick.AddListener(OpenPlayerInventory);
    }

    // �÷��̾� ����â ����
    private void OpenPlayerStatus()
    {
        GameUIClass uiPlayerStatus = UIManager.Instance.GetUIComponent<UIPlayerStatus>();
        uiPlayerStatus.OpenUI();
    }

    // �÷��̾� �κ��丮 ����
    private void OpenPlayerInventory()
    {
        GameUIClass uiPlayerInventory = UIManager.Instance.GetUIComponent<UIPlayerInventory>();
        uiPlayerInventory.OpenUI();
    }
}
