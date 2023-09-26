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

    // Move, Look, Fire �̺�Ʈ�� ����Ǹ� ȣ��Ǵ� �Լ�
    // InputValue�� �޾ƿ�
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
        // UI�� ��ǥ���� ���� �� ��ǥ�� ��ȯ
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        // A���� - B���� = B -> A�� ���ϴ� ����
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }

    public void OnFire(InputValue value)
    {
        //Debug.Log("OnFire" + value.ToString());
        IsAttacking = value.isPressed;  // Ű�� ������ �ִ°�
    }
}
