using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayer1Goal;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Load 해오므로 (Clone)이 붙어서 Ball과 Equals가 아닌 Contains로 검사
        if(collision.name.Contains("Ball"))
        {
            if(isPlayer1Goal)
            {
                Debug.Log("Player 2 Scored");
                gameManager.Player2Scored();
            }
            else
            {
                Debug.Log("Player 1 Scored");
                gameManager.Player1Scored();
            }
        }
    }
}
