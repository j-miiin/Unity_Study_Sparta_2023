using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayerCharacterBtn : MonoBehaviour
{
    public Player player;
    public GameObject playerSelectPanel;
    public void SelectCharacter1()
    {
        player.SetPlayerCharacter(PlayerCharacterType.ONE);
        playerSelectPanel.SetActive(false);
    }

    public void SelectCharacter2()
    {
        player.SetPlayerCharacter(PlayerCharacterType.TWO);
        playerSelectPanel.SetActive(false);
    }
}
