using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    // 참가자 이름 리스트 컨테이너
    public Transform contentContainer;
    public GameObject attendeeNameContainer;

    // 참가자 리스트
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
            playerName.GetComponent<TMP_Text>().text = i + "번째";
            playerName.transform.SetParent(contentContainer);
        }
    }
}
