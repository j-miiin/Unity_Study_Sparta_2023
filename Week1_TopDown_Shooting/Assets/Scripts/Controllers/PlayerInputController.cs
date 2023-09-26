using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownCharacterController
{
    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    // Move, Look, Fire 이벤트가 실행되면 호출되는 함수
    // InputValue를 받아옴
    public void OnMove(InputValue value)
    {
        //Debug.Log("OnMove" + value.ToString());
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        //Debug.Log("OnLook" + value.ToString());
        Vector2 newAim = value.Get<Vector2>();
        // UI상 좌표에서 게임 내 좌표로 변환
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        // A벡터 - B벡터 = B -> A로 향하는 벡터
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }

    public void OnFire(InputValue value)
    {
        //Debug.Log("OnFire" + value.ToString());
        IsAttacking = value.isPressed;  // 키가 눌려져 있는가
    }
}
