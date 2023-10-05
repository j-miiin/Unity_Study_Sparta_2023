using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    // �̵� ó���� base���� �ϰ� �����Ƿ� �ִϸ��̼� ó���� �ϸ� ��
    public override void Enter()
    {
        // Run�� �̵� �ӵ��� ����
        stateMachine.MovementSpeedModifier = groundData.RunSpeedModifier;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }
}
