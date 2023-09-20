using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class UIPlayerInfo : GameUIClass
{
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private TMP_Text _playerLevelText;
    [SerializeField] private TMP_Text _playerGoldText;
    //[SerializeField] private Image _playerCharacterImage;

    private UIController _controller;

    private void Start()
    {
        _controller = UIManager.Instance.controller;
        _controller.OnOpenPlayerInventoryEvent += CloseUI;
        _controller.OnClosePlayerInventoryEvent += OpenUI;
    }

    // player 객체를 받아와서 UI에 플레이어 정보 set
    public void SetPlayerInfo(PlayerDTO player)
    {
        _playerNameText.text = player.Name;
        _playerLevelText.text = "Lv. " + player.Level.ToString();
        _playerGoldText.text = player.Gold.ToString("C");
    }
}
