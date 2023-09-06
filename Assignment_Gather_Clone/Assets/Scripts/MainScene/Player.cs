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

    private string name;

    public string Name { get { return name; } }

    private void Awake()
    {
        name = PlayerPrefs.GetString(PLAYER_NAME);
        if (PlayerPrefs.GetInt(PLAYER_CHARACTER) == 1) SetPlayerCharacterAnimator(PlayerCharacterType.ONE);
        else SetPlayerCharacterAnimator(PlayerCharacterType.TWO);
    }

    public void SetPlayerName(string name)
    {
        this.name = name;
        PlayerPrefs.SetString(PLAYER_NAME, name);
    }

    public void SetPlayerCharacter(PlayerCharacterType type)
    {
        SetPlayerCharacterAnimator(type);
        PlayerPrefs.GetInt(PLAYER_CHARACTER, (int)type);
    }

    private void SetPlayerCharacterAnimator(PlayerCharacterType type)
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
    }
}
