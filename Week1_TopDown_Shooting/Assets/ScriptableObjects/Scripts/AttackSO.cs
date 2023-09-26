using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackData", menuName = "TopDownController/Attacks/Default", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float size;      // 공격 크기
    public float delay;     // 공격 딜레이
    public float power;     // 공격 데미지
    public float speed;     // 공격 속도
    public LayerMask target;

    [Header("Knock Back Info")]
    public bool isOnKnockback;
    public float knockbackPower;
    public float knockbackTime;
}
