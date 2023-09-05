using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPlayerCharacterPanelBtn : MonoBehaviour
{
    public GameObject playerCharacterPanel;

    public void OpenPlayerCharacterPanel()
    {
        playerCharacterPanel.SetActive(true);
    }
}
