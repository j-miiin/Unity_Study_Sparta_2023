using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    // Ground를 상속 받은 State들은 모두 Ground가 켜져 있는 상태로 시작하게 됨
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

        // 땅에 있지 않을 때
        // AND velocity.y에 작용하는 값이 우리가 gravity를 한 fixedDeltaTime에 적용하는 값보다 작을 때
        // (== 훨씬 더 빠른 속도로 떨어질 때)
        if (!stateMachine.Player.Controller.isGrounded
            && stateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.fixedDeltaTime)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // 입력이 들어오지 않은 경우
        if (stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }

        // 입력 키가 떼어졌을 때
        // Ground에서 정의하는 이유 : 다른 State에서 키를 뗐을 때는 다른 동작을 해야 함
        // Ground에서 키를 떼면 어떻게 할 것이냐를 정의
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
