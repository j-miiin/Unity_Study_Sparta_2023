using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetPlayerName : MonoBehaviour
{
    const string PLAYER_NAME_INPUT_FIELD = "PlayerNameInputField";

    private InputField playerNameInputField;
    private string playerName = null;

    private void Awake()
    {
        playerNameInputField = GameObject.Find(PLAYER_NAME_INPUT_FIELD).transform.GetComponent<InputField>();
        playerName = playerNameInputField.GetComponent<InputField>().text;
    }

    void Update()
    {
        if (playerName.Length > 1 && Input.GetKeyDown(KeyCode.Return))
        {
            SavePlayerName();
        } 
    }

    public void SavePlayerName()
    {
        playerName = playerNameInputField.text;
        if (playerName.Length > 0)
        {
            PlayerPrefs.SetString(Player.PLAYER_NAME, playerName);
            SceneManager.LoadScene("MainScene");
        }
    }
}
