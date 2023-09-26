using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAnimationController : TopDownAnimations
{
    // StringToHash : 특정 문자열을 일정한 공식에 의해 Hash 값으로 변환 
    // 문자열 연산은 비용이 비싸기 때문에 Hash 값으로 비교
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");

    private HealthSystem _healthSystem;

    protected override void Awake()
    {
        base.Awake();
        _healthSystem = GetComponent<HealthSystem>();  
    }

    void Start()
    {
        controller.OnAttackEvent += Attacking;
        controller.OnMoveEvent += Move;

        if (_healthSystem != null)
        {
            _healthSystem.OnDamage += Hit;
            _healthSystem.OnInvincibilityEnd += InvicibilityEnd;
        }
    }

    private void Attacking(AttackSO obj)
    {
        animator.SetTrigger(Attack);
    }

    private void Move(Vector2 obj)
    {
        animator.SetBool(IsWalking, obj.magnitude > .5f);
    }

    private void Hit()
    {
        animator.SetBool(IsHit, true);
    }

    private void InvicibilityEnd()
    {
        animator.SetBool(IsHit, false);
    }
}
