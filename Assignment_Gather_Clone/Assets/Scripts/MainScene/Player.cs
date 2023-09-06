using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public const string PLAYER_NAME = "PlayerName";
    public const string PLAYER_CHARACTER = "PlayerCharacter";
    const string PLAYER_CHARACTER_SPRITE_1 = "my_pixel_gather_character_160";
    const string PLAYER_CHARACTER_SPRITE_2 = "pixelcat_grey";

    private string name;
    private Sprite characterSprite1;
    private Sprite characterSprite2;

    public string Name { get { return name; } }

    private void Awake()
    {
        name = PlayerPrefs.GetString(PLAYER_NAME);
        characterSprite1 = Resources.Load<Sprite>(PLAYER_CHARACTER_SPRITE_1);
        characterSprite2 = Resources.Load<Sprite>(PLAYER_CHARACTER_SPRITE_2);
        if (PlayerPrefs.GetInt(PLAYER_CHARACTER) == 1) transform.GetComponentInChildren<SpriteRenderer>().sprite = characterSprite1;
        else transform.GetComponentInChildren<SpriteRenderer>().sprite = characterSprite2;
    }

    public void SetPlayerName(string name)
    {
        this.name = name;
        PlayerPrefs.SetString(PLAYER_NAME, name);
    }

    public void SetPlayerCharacter(PlayerCharacterType type)
    {
        if (type == PlayerCharacterType.ONE)
        {
            transform.GetComponentInChildren<Animator>().runtimeAnimatorController 
                = (RuntimeAnimatorController)Resources.Load("AnimController/PlayerCharacterImage");
        }
        else
        {
            transform.GetComponentInChildren<Animator>().runtimeAnimatorController 
                = (RuntimeAnimatorController)Resources.Load("AnimController/PlayerCharacter2AnimController");
        }

        PlayerPrefs.GetInt(PLAYER_CHARACTER, (int)type);
    }
}
