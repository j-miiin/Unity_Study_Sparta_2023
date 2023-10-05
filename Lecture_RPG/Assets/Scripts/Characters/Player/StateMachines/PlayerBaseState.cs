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
        AddInputActionsCallback();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallback();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Update()
    {
        Move();
    }

    // InputAction�� �ɾ��� Callback
    protected virtual void AddInputActionsCallback()
    {

    }

    protected virtual void RemoveInputActionsCallback()
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
