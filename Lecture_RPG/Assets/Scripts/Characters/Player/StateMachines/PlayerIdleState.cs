using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IState�� ��� �޾Ƽ� �����ϰ� �Ǹ� �ߺ��Ǵ� ��ɵ��� �������� ��
// ���ο� BaseState�� ��� �޵��� ����
public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;    // Idle�� ���� �������� ����
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

        // key �Է��� �Ǹ� Walk�� Run���� ��ȯ
        if (stateMachine.MovementInput != Vector2.zero)
        {
            OnMove();
            return;
        }
    }
}
