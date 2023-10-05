using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// ��� State�� StateMachine�� �������� ����
public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.Data.GroundedData;
    }

    // �� State�� �� �� �� State���� �ʿ��� InputAction�� ���� Callback�� ����/����
    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
    }

    // InputAction�� �ɾ��� Callback
    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled += OnMoveCanceled;
        input.PlayerActions.Run.started += OnRunStarted;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMoveCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;
    }

    protected virtual void OnRunStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }

    protected virtual void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }

    private void ReadMovementInput()
    {
        // �̷� ��� ���� ���̴� �͵��� ĳ���ϴ°� ����
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }

    // ī�޶� �ٶ󺸴� �������� �̵��� ����
    private Vector3 GetMovementDirection()
    {
        // ���� ī�޶� �ٶ󺸴� ����� ������
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        // y ���� �����ؾ� ���� ���鼭 ���� ����
        forward.y = 0;
        right.y = 0;

        // Normalize�� Normalized�� ����
        // Normalize�� ���� ��ü�� Normalize�ϴ� ��
        // Normalized�� ���͸� Normalized�ؼ� ������ ���� ��
        forward.Normalize();
        right.Normalize();

        // ������ ���� ����, �������� ���� ���Ϳ� �̵� ������ ���ؼ� �̵�
        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Player.Controller.Move((movementDirection * movementSpeed) * Time.deltaTime);
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection); // movementDirection�� �ٶ󺸴� ���ʹϾ�
            // �����̵� �ϵ��� ���� ���� �ʰ� �ϱ� ���� Ÿ�� � ����
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation,
                stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    // �ִϸ��̼��� Ű�ų� ��
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }
}
