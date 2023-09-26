using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsChangeType
{
    Add,        // ���ϱ�
    Multiple,   // ���ϱ�
    Override,   �����
}

// ������ �𸣱� ������ Inspector���� ���̷��� Serializable�� �޾���� ��
[Serializable]
public class CharacterStats 
{
    public StatsChangeType statsChangeType; // Ÿ��
    [Range(1, 100)] public int maxHealth;   // Inspector���� ������ �� �ְ� ���� ����
    [Range(1f, 20f)] public float speed;    // �̵��ӵ�

    // ���� ������
    public AttackSO attackSO;
}
