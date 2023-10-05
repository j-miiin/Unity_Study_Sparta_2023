using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IState를 상속 받아서 구현하게 되면 중복되는 기능들이 많아지게 됨
// 새로운 BaseState를 상속 받도록 구현
public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;    // Idle일 때는 움직이지 않음
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Exit() {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }
    
    public override void Update()
    {
        base.Update();

        // key 입력이 되면 Walk나 Run으로 전환
        if (stateMachine.MovementInput != Vector2.zero)
        {
            OnMove();
            return;
        }
    }
}
