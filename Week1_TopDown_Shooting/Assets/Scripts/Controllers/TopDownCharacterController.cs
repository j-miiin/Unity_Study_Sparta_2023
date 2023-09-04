using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    // event 외부에서는 호출하지 못하게 막든다
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
}

// 이동 연습 코드
//[SerializeField] private float speed = 5f;


// Update is called once per frame
//void Update()
//{
    //float x = Input.GetAxisRaw("Horizontal");
    //float y = Input.GetAxisRaw("Vertical");

    //transform.position += new Vector3(x, y) * speed * Time.deltaTime;
//}