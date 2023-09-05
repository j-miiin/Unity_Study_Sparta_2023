using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    // ������ �̸� ����Ʈ �����̳�
    public Transform contentContainer;
    public GameObject attendeeNameContainer;

    // ������ ����Ʈ
    private GameObject[] attendeeList;

    private void Awake()
    {
        I = this;
    }

    void Start()
    {
        LoadAttendeeList();
    }

    void Update()
    {
        
    }

    private void LoadAttendeeList()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject playerName = Instantiate(attendeeNameContainer);
            playerName.GetComponent<TMP_Text>().text = i + "��°";
            playerName.transform.SetParent(contentContainer);
        }
    }
}
