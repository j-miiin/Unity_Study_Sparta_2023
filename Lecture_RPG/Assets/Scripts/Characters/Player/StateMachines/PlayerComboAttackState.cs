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

        // 재사용 해야 되는 값을 초기화
        alreadyApplyCombo = false;
        alreadyAppliedForce = false;

        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfo(comboIndex); // 현재 사용해야 되는 콤보 인덱스 정보
        stateMachine.Player.Animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        // 공격 중 콤보를 성공하지 못하면
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

        // 내가 받고 있힘을 리셋하고 AddForce 진행
        stateMachine.Player.ForceReceiver.Reset();

        // 정면에서 밀려남
        stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward * attackInfoData.Force);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        
        // 이미 애니메이션 처리 중
        if (normalizedTime < 1f)
        {
            // 힘을 적용해야 되는 시간보다 커지면
            if (normalizedTime >= attackInfoData.ForceTransitionTime)
            {
                // 힘 적용
                TryApplyForce();
            }
            if (normalizedTime >= attackInfoData.ComboTransitionTime)
            {
                // 콤보 적용 
                TryApplyForce();
            }
        }
        // 애니메이션의 모든 플레이가 완료됐을 때
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
