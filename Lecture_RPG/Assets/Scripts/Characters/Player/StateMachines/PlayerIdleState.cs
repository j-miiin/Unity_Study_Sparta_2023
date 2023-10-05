using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IState�� ��� �޾Ƽ� �����ϰ� �Ǹ� �ߺ��Ǵ� ��ɵ��� �������� ��
// ���ο� BaseState�� ��� �޵��� ����
public class PlayerIdleState : PlayerBaseState
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
        base.Enter();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }
    
    public override void Update()
    {
        base.Update();
    }
}
