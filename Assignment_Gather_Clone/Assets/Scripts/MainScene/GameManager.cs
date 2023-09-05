using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    public const string PLAYER_NAME = "PlayerName";
    public const string PLAYER_CHARACTER = "PlayerCharacter";

    public TMP_Text playerNameText;
    public GameObject playerCharacter;
    public Sprite characterSprite1;
    public Sprite characterSprite2;

    private void Awake()
    {
        I = this;
    }
    void Start()
    {
        playerNameText.text = PlayerPrefs.GetString(PLAYER_NAME);
        if (PlayerPrefs.GetInt(PLAYER_CHARACTER) == 1) playerCharacter.GetComponent<SpriteRenderer>().sprite = characterSprite1;
        else playerCharacter.GetComponent<SpriteRenderer>().sprite = characterSprite2;
    }

    void Update()
    {
        
    }
}
