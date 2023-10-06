using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
    }

    // ������ ����������(���� ��ư�� ���� �߿��� ���� ���� �߿��� ��)
    // Ư�� ���¿����� ����� Ű�� ������ ����
    // �� ��Ȳ�� ū ���µ�(Ground, Air, Attack)���� override �ؼ� �����ϴ� ����� ����

    // InputAction�� �ɾ��� Callback
    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled += OnMovementCanceled;
        input.PlayerActions.Run.started += OnRunStarted;

        input.PlayerActions.Jump.started += OnJumpStarted;

        input.PlayerActions.Attack.performed += OnAttackPerformed;
        input.PlayerActions.Attack.canceled += OnAttackCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;

        input.PlayerActions.Jump.started -= OnJumpStarted;

        input.PlayerActions.Attack.performed -= OnAttackPerformed;
        input.PlayerActions.Attack.canceled -= OnAttackCanceled;
    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnAttackPerformed(InputAction.CallbackContext go)
    {
        stateMachine.IsAttacking = true;
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext go)
    {
        stateMachine.IsAttacking = false;
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
        // ���� ī�޶� �ٶ󺸴� ����� ������
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
        stateMachine.Player.Controller.Move(
            ((movementDirection * movementSpeed)
            + stateMachine.Player.ForceReceiver.Movement)
            * Time.deltaTime);
    }

    // direction ���� ���� �ʰ� ForceReceiver�� ���� Movement ������ �̵�
    protected void ForceMove()
    {
        stateMachine.Player.Controller.Move(stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
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
}
