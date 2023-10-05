using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    // Ground�� ��� ���� State���� ��� Ground�� ���� �ִ� ���·� �����ϰ� ��
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // ���� ���� ���� ��
        // AND velocity.y�� �ۿ��ϴ� ���� �츮�� gravity�� �� fixedDeltaTime�� �����ϴ� ������ ���� ��
        // (== �ξ� �� ���� �ӵ��� ������ ��)
        if (!stateMachine.Player.Controller.isGrounded
            && stateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.fixedDeltaTime)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // �Է��� ������ ���� ���
        if (stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }

        // �Է� Ű�� �������� ��
        // Ground���� �����ϴ� ���� : �ٸ� State���� Ű�� ���� ���� �ٸ� ������ �ؾ� ��
        // Ground���� Ű�� ���� ��� �� ���̳ĸ� ����
        stateMachine.ChangeState(stateMachine.IdleState);

        base.OnMovementCanceled(context);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.JumpState);
    }

    protected virtual void OnMove()
    {
        stateMachine.ChangeState(stateMachine.WalkState);
    }
}
