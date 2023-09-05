using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttendeeListBtn : MonoBehaviour
{
    public GameObject attendeeListPanel;

    public void OpenAttendeeList()
    {
        attendeeListPanel.SetActive(true);
    }

    public void CloseAttendeeList()
    {
        attendeeListPanel.SetActive(false);
    }
}
