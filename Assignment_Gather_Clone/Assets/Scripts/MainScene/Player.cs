using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const string PLAYER_NAME = "PlayerName";
    public const string PLAYER_CHARACTER = "PlayerCharacter";

    // 플레이어 이름, 캐릭터
    public TMP_Text playerNameText;
    public GameObject playerCharacter;
    public Sprite characterSprite1;
    public Sprite characterSprite2;

    private void Awake()
    {
        playerNameText.text = PlayerPrefs.GetString(PLAYER_NAME);
        if (PlayerPrefs.GetInt(PLAYER_CHARACTER) == 1) playerCharacter.GetComponent<SpriteRenderer>().sprite = characterSprite1;
        else playerCharacter.GetComponent<SpriteRenderer>().sprite = characterSprite2;
    }

    public void SetPlayerName(string name)
    {
        playerNameText.text = name;
        PlayerPrefs.SetString(PLAYER_NAME, name);

    }

    public void SetPlayerCharacter(PlayerCharacterType type)
    {
        if (type == PlayerCharacterType.ONE) playerCharacter.GetComponent<SpriteRenderer>().sprite = characterSprite1;
        else playerCharacter.GetComponent<SpriteRenderer>().sprite = characterSprite2;

        PlayerPrefs.GetInt(PLAYER_CHARACTER, (int)type);
    }
}
