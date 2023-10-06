using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

// 모든 State는 StateMachine과 역참조를 진행
public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.Data.GroundedData;
    }

    // 각 State에 들어갈 때 그 State에서 필요한 InputAction에 대한 Callback을 연결/해지
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

    // 지금은 공용이지만(공격 버튼을 공격 중에도 쓰고 점프 중에도 씀)
    // 특정 상태에서만 사용할 키를 구현할 때는
    // 각 상황의 큰 상태들(Ground, Air, Attack)에서 override 해서 구현하는 방법도 잇음

    // InputAction에 걸어줄 Callback
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
        // 이런 경우 자주 쓰이는 것들을 캐싱하는게 좋음
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }

    // 카메라가 바라보는 방향으로 이동할 것임
    private Vector3 GetMovementDirection()
    {
        // 메인 카메라가 바라보는 정면과 오른쪽
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        // y 값을 제거해야 땅을 보면서 가지 않음
        forward.y = 0;
        right.y = 0;

        // Normalize와 Normalized의 차이
        // Normalize는 벡터 자체를 Normalize하는 것
        // Normalized는 벡터를 Normalized해서 가지고 오는 것
        forward.Normalize();
        right.Normalize();

        // 앞으로 가는 벡터, 우측으로 가는 벡터에 이동 방향을 곱해서 이동
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

    // direction 값을 받지 않고 ForceReceiver가 가진 Movement 값으로 이동
    protected void ForceMove()
    {
        stateMachine.Player.Controller.Move(stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection); // movementDirection을 바라보는 쿼터니언
            // 순간이동 하듯이 휙휙 돌지 않게 하기 위해 타원 곡선 보간
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation,
                stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    // 애니메이션을 키거나 끔
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
        AnimatorStateInfo nextInfo = animator.GetCurrentAnimatorStateInfo(0);   // 다음 애니메이션에 대한 정보

        // 현재 이 애니메이션은 transition line을 타고 있는가
        // tag가 attack인가
        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            // 0부터 1까지의 값 중 몇 퍼센트의 값으로 처리 중인가
            return nextInfo.normalizedTime;
        } 
        // 내 tag가 attack인가
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        // Attack이 아닐 때
        else
        {
            return 0f;
        }
    }
}
