using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsChangeType
{
    Add,
    Mutiple,
    Override,
}

[Serializable]
public class CharacterStats 
{
    public StatsChangeType statsChangeType;
    [Range(1, 100)] public int maxHealth;   // Inspector���� ������ �� �ְ� ���� ����
    [Range(1f, 20f)] public float speed;

    // ���� ������
    public AttackSO attackSO;
}