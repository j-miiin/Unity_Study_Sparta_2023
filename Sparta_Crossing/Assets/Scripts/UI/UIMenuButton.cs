using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuButton : MonoBehaviour
{
    [SerializeField] private Button _playerStatusButton;
    [SerializeField] private Button _playerInventoryButton;

    private void Start()
    {
        _playerStatusButton.onClick.AddListener(OpenPlayerStatus);
        _playerInventoryButton.onClick.AddListener(OpenPlayerInventory);
    }

    private void OpenPlayerStatus()
    {
        GameUIClass uiPlayerStatus = UIManager.Instance.GetUIComponent<UIPlayerStatus>();
        uiPlayerStatus.OpenUI();
    }

    private void OpenPlayerInventory()
    {
        GameUIClass uiPlayerInventory = UIManager.Instance.GetUIComponent<UIPlayerInventory>();
        uiPlayerInventory.OpenUI();
    }
}
