using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyAppliedForce;
    private bool alreadyApplyCombo;

    AttackInfoData attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        // ���� �ؾ� �Ǵ� ���� �ʱ�ȭ
        alreadyApplyCombo = false;
        alreadyAppliedForce = false;

        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfo(comboIndex); // ���� ����ؾ� �Ǵ� �޺� �ε��� ����
        stateMachine.Player.Animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        // ���� �� �޺��� �������� ���ϸ�
        if (!alreadyApplyCombo)
        {
            stateMachine.ComboIndex = 0;
        }
    }

    private void TryComboAttack()
    {
        if (alreadyApplyCombo) return;

        if (attackInfoData.ComboStateIndex == -1) return;

        if (!stateMachine.IsAttacking) return;

        alreadyApplyCombo = true;
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        // ���� �ް� ������ �����ϰ� AddForce ����
        stateMachine.Player.ForceReceiver.Reset();

        // ���鿡�� �з���
        stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward * attackInfoData.Force);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        
        // �̹� �ִϸ��̼� ó�� ��
        if (normalizedTime < 1f)
        {
            // ���� �����ؾ� �Ǵ� �ð����� Ŀ����
            if (normalizedTime >= attackInfoData.ForceTransitionTime)
            {
                // �� ����
                TryApplyForce();
            }
            if (normalizedTime >= attackInfoData.ComboTransitionTime)
            {
                // �޺� ���� 
                TryApplyForce();
            }
        }
        // �ִϸ��̼��� ��� �÷��̰� �Ϸ���� ��
        else
        {
            if (alreadyApplyCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            } else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }
}
