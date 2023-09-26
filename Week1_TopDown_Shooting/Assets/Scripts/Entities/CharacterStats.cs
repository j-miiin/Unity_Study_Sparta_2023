using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsChangeType
{
    Add,        // 더하기
    Multiple,   // 곱하기
    Override,   덮어쓰기
}

// 구조를 모르기 때문에 Inspector에서 보이려면 Serializable을 달아줘야 함
[Serializable]
public class CharacterStats 
{
    public StatsChangeType statsChangeType; // 타입
    [Range(1, 100)] public int maxHealth;   // Inspector에서 조절할 수 있게 만들 것임
    [Range(1f, 20f)] public float speed;    // 이동속도

    // 공격 데이터
    public AttackSO attackSO;
}
