using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInfo : GameUIClass
{
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private TMP_Text _playerLevelText;
    [SerializeField] private TMP_Text _playerGoldText;
    //[SerializeField] private Image _playerCharacterImage;

    // player ��ü�� �޾ƿͼ� UI�� �÷��̾� ���� set
    public void SetPlayerInfo(PlayerDTO player)
    {
        _playerNameText.text = player.Name;
        _playerLevelText.text = "Lv. " + player.Level.ToString();
        _playerGoldText.text = player.Gold.ToString("C");
    }
}