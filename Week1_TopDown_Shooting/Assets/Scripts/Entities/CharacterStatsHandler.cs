using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    private const float MinAttackDelay = 0.03f;
    private const float MinAttackPower = 0.5f;
    private const float MinAttackSize = 0.4f;
    private const float MinAttackSpeed = .1f;
    private const float MinSpeed = 0.8f;
    private const int MinMaxHealth = 5;

    [SerializeField] private CharacterStats baseStats;

    public CharacterStats CurrentStats { get; private set; }
    public List<CharacterStats> statsModifiers = new List<CharacterStats>();

    private void Awake()
    {
        UpdateCharacterStats();
    }

    public void AddStatModifier(CharacterStats statModifier)
    {
        statsModifiers.Add(statModifier);
        UpdateCharacterStats();
    }

    public void RemoveStatModifier(CharacterStats statModifier)
    {
        statsModifiers.Remove(statModifier);
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO); // baseStat을 복제 -> 자유롭게 수정하기 위해
        }

        CurrentStats = new CharacterStats { attackSO = attackSO };  // 생성하면서 초기화는 중괄호
        UpdateStats((a, b) => b, baseStats);    //a, b를 받아서 후자를 사용 -> CurrentStat에 baseStat을 덮어씌움
        if (CurrentStats.attackSO != null)
        {
            CurrentStats.attackSO.target = baseStats.attackSO.target;
        }

        foreach (CharacterStats modifier in statsModifiers.OrderBy(o => o.statsChangeType))
        {
            if (modifier.statsChangeType == StatsChangeType.Override)
            {
                UpdateStats((o, o1) => o1, modifier);
            } 
            else if (modifier.statsChangeType == StatsChangeType.Add)
            {
                UpdateStats((o, o1) => o + o1, modifier);
            } else if (modifier.statsChangeType == StatsChangeType.Multiple)
            {
                UpdateStats((o, o1) => o * o1, modifier);
            }
        }

        LimitAllStats();
    }

    private void UpdateStats(Func<float, float, float> operation, CharacterStats newModifier)
    {
        CurrentStats.maxHealth = (int)operation(CurrentStats.maxHealth, newModifier.maxHealth);
        CurrentStats.speed = operation(CurrentStats.speed, newModifier.speed);

        if (CurrentStats.attackSO == null || newModifier.attackSO == null) return;

        UpdateAttackStats(operation, CurrentStats.attackSO, newModifier.attackSO);

        if (CurrentStats.attackSO.GetType() != newModifier.attackSO.GetType())
        {
            return;
        }

        switch (CurrentStats.attackSO)
        {
            case RangedAttackData _:
                ApplyRangedStats(operation, newModifier);
                break;
        }
    }

    private void UpdateAttackStats(Func<float, float, float> operation, AttackSO currentAttack, AttackSO newAttack)
    {
        if (currentAttack == null || newAttack == null) return;

        currentAttack.delay = operation(currentAttack.delay, newAttack.delay);
        currentAttack.power = operation(currentAttack.power, newAttack.power);
        currentAttack.size = operation(currentAttack.size, newAttack.size);
        currentAttack.speed = operation(currentAttack.speed, newAttack.speed);
    }

    private void ApplyRangedStats(Func<float, float, float> operation, CharacterStats newModifier)
    {
        RangedAttackData currentRangedAttackData = (RangedAttackData)CurrentStats.attackSO;

        if (!(newModifier.attackSO is RangedAttackData)) return;

        RangedAttackData rangedAttackModifier = (RangedAttackData)newModifier.attackSO;
        currentRangedAttackData.multipleProjectilesAngle =
            operation(currentRangedAttackData.multipleProjectilesAngle, rangedAttackModifier.multipleProjectilesAngle);
        currentRangedAttackData.spread =
            operation(currentRangedAttackData.spread, rangedAttackModifier.spread);
        currentRangedAttackData.duration =
            operation(currentRangedAttackData.duration, rangedAttackModifier.duration);
        currentRangedAttackData.numberOfProjectilesPerShot =
            Mathf.CeilToInt(operation(currentRangedAttackData.numberOfProjectilesPerShot, rangedAttackModifier.numberOfProjectilesPerShot));
        currentRangedAttackData.projectileColor =
            UpdateColor(operation, currentRangedAttackData.projectileColor, rangedAttackModifier.projectileColor);
    }

    private Color UpdateColor(Func<float, float, float> operation, Color currentColor, Color newColor)
    {
        return new Color(
            operation(currentColor.r, newColor.r),
            operation(currentColor.g, newColor.g),
            operation(currentColor.b, newColor.b),
            operation(currentColor.a, newColor.a)
            );
    }

    private void LimitStats(ref float stat, float minVal)
    {
        stat = Mathf.Max(stat, minVal);
    }

    private void LimitAllStats()
    {
        if (CurrentStats == null || CurrentStats.attackSO == null) return;

        LimitStats(ref CurrentStats.attackSO.delay, MinAttackDelay);
        LimitStats(ref CurrentStats.attackSO.power, MinAttackPower);
        LimitStats(ref CurrentStats.attackSO.size, MinAttackSize);
        LimitStats(ref CurrentStats.attackSO.speed, MinAttackSpeed);
        LimitStats(ref CurrentStats.speed, MinSpeed);
        CurrentStats.maxHealth = Mathf.Max(CurrentStats.maxHealth, MinMaxHealth);
    }
}
