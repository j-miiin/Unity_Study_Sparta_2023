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

    public void SetPlayerInfo(Player player)
    {
        _playerNameText.text = player.Name;
        _playerLevelText.text = player.Level.ToString();
        _playerGoldText.text = player.Gold.ToString();
    }
}
