using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetPlayerName : MonoBehaviour
{
    public TMP_InputField playerNameInputField;
    private string playerName = null;


    private void Awake()
    {
        playerName = playerNameInputField.GetComponent<TMP_InputField>().text;
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
            PlayerPrefs.SetString(GameManager.PLAYER_NAME, playerName);
            SceneManager.LoadScene("MainScene");
        }
    }
}
