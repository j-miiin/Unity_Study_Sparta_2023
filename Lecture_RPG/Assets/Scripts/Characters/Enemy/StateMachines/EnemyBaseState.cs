using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
        groundData = stateMachine.Enemy.Data.GroundedData;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }

    protected void ForceMove()
    {
        stateMachine.Enemy.Controller.Move(stateMachine.Enemy.ForceReceiver.Movement * Time.deltaTime);
    }

    private void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Enemy.Controller.Move(
            ((movementDirection * movementSpeed)
            + stateMachine.Enemy.ForceReceiver.Movement)
            * Time.deltaTime);
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            movementDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection); // movementDirection�� �ٶ󺸴� ���ʹϾ�
            // �����̵� �ϵ��� ���� ���� �ʰ� �ϱ� ���� Ÿ�� � ����
            stateMachine.Enemy.transform.rotation = Quaternion.Slerp(
                stateMachine.Enemy.transform.rotation,
                targetRotation,
                stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    private Vector3 GetMovementDirection()
    {
        return (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).normalized;
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetCurrentAnimatorStateInfo(0);   // ���� �ִϸ��̼ǿ� ���� ����

        // ���� �� �ִϸ��̼��� transition line�� Ÿ�� �ִ°�
        // tag�� attack�ΰ�
        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            // 0���� 1������ �� �� �� �ۼ�Ʈ�� ������ ó�� ���ΰ�
            return nextInfo.normalizedTime;
        }
        // �� tag�� attack�ΰ�
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        // Attack�� �ƴ� ��
        else
        {
            return 0f;
        }
    }

    protected bool IsInChasingRange()
    {
        if (stateMachine.Target.IsDead) { return false; }

        // sqrMagnitude�� ������ ������ ���� ũ��
        // ������ Ǫ�� ������ ���̱� ������ Ǯ�� �ʰ� ���ϱ⸦ �� �� �� �ؼ� ���ϴ� ��
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Enemy.Data.PlayerChasingRange * stateMachine.Enemy.Data.PlayerChasingRange;
    }
}
