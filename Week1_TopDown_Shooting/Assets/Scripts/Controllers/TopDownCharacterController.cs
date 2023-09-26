using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    // event 외부에서는 호출하지 못하게 막는다
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;

    // 마지막으로 공격한 시간
    private float _timeSinceLastAttack = float.MaxValue;
    protected bool IsAttacking { get; set; }

    protected CharacterStatsHandler Stats { get; private set; }

    protected virtual void Awake()
    {
        Stats = GetComponent<CharacterStatsHandler>();
    }

    protected virtual void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (Stats.CurrentStats.attackSO == null) return;    // attack 정보가 없으므로 공격을 하지 않음

        if (_timeSinceLastAttack <= Stats.CurrentStats.attackSO.delay)  // 현재 공격 딜레이보다 시간이 적으면
        {   
            _timeSinceLastAttack += Time.deltaTime;     // 시간 증가
        }

        // 마우스가 눌리는 동안 딜레이보다 마지막으로 공격한 시간이 커지면 
        if (IsAttacking && _timeSinceLastAttack > Stats.CurrentStats.attackSO.delay)
        {
            _timeSinceLastAttack = 0;
            CallAttackEvent(Stats.CurrentStats.attackSO);
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    public void CallAttackEvent(AttackSO attackSO)
    {
        OnAttackEvent?.Invoke(attackSO);
    }
}

// 이동 연습 코드
//[SerializeField] private float speed = 5f;


// Update is called once per frame
//void Update()
//{
    //float x = Input.GetAxisRaw("Horizontal");
    //float y = Input.GetAxisRaw("Vertical");

    //transform.position += new Vector3(x, y) * speed * Time.deltaTime;
//}