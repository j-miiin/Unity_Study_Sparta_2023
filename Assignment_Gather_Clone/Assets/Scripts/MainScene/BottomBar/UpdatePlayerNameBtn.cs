using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdatePlayerNameBtn : MonoBehaviour
{
    public Player player;
    public TMP_InputField playerNameInputField;
    public GameObject updatePlayerNamePanel;
    private string playerName = null;

    public void UpdatePlayerName()
    {
        playerName = playerNameInputField.GetComponent<TMP_InputField>().text;
        if (playerName.Length > 0)
        {
            player.SetPlayerName(playerName);
            playerNameInputField.text = "";
            updatePlayerNamePanel.SetActive(false);
        }
    }
}
