using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackInfoData
{
    // 공격 이름
    [field: SerializeField] public string AttackName { get; private set; }
    // 인덱스
    [field: SerializeField] public int ComboStateIndex { get; private set; }
    // 콤보를 유지하려면 언제까지 때려야 하는지
    [field: SerializeField][field: Range(0f, 1f)] public float ComboTransitionTime { get; private set; }
    // 힘을 언제 클릭하면 언제 적용할 것인지
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }
    // 데미지
    [field: SerializeField] public int Damage { get; private set; }
}


[Serializable]
public class PlayerAttackData
{
    [field: SerializeField] public List<AttackInfoData> AttackInfoDatas { get; private set; }
    public int GetAttackInfoCount() { return AttackInfoDatas.Count; }
    public AttackInfoData GetAttackInfo(int index) { return AttackInfoDatas[index]; }
}
