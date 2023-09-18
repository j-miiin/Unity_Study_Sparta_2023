using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsChangeType
{
    Add,
    Multiple,
    Override,
}

[Serializable]
public class CharacterStats 
{
    public StatsChangeType statsChangeType;
    [Range(1, 100)] public int maxHealth;   // Inspector에서 조절할 수 있게 만들 것임
    [Range(1f, 20f)] public float speed;

    // 공격 데이터
    public AttackSO attackSO;
}
