using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUpdatePlayerNamePanelBtn : MonoBehaviour
{
    public GameObject updatePlayerNamePanel;

    public void OpenUpdatePlayerNamePanel()
    {
        updatePlayerNamePanel.SetActive(true);
    }
}
