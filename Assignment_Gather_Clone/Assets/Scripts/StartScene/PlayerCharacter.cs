using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    public GameObject playerSelectPanel;
    public Image playerCharacterImage;
    public Sprite characterSprite1;
    public Sprite characterSprite2;

    void Update()
    {
         if (!PlayerPrefs.HasKey(Player.PLAYER_CHARACTER) || PlayerPrefs.GetInt(Player.PLAYER_CHARACTER) == 1)
        {
            playerCharacterImage.sprite = characterSprite1;
        } else
        {
            playerCharacterImage.sprite = characterSprite2;
        }
    }

    public void OpenCharacterSelectPanel()
    {
        playerSelectPanel.SetActive(true);
    }

    public void SelectCharacter1()
    {
        PlayerPrefs.SetInt(Player.PLAYER_CHARACTER, 1);
        playerSelectPanel.SetActive(false);
    }

    public void SelectCharacter2()
    {
        PlayerPrefs.SetInt(Player.PLAYER_CHARACTER, 2);
        playerSelectPanel.SetActive(false);
    }
}
