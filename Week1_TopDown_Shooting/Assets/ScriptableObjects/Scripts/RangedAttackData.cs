using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackData", menuName = "TopDownController/Attacks/Ranged", order = 1)]
public class RangedAttackData : AttackSO
{
    [Header("Ranged Attack Data")]
    public string bulletNameTag;            // ÃÑ¾Ë ÀÌ¸§
    public float duration;                  
    public float spread;                    // Åº ÆÛÁü
    public int numberOfProjectilesPerShot;  // ÇÑ ¹ø¿¡ ½î´Â ÃÑ¾Ë ¼ö
    public float multipleProjectilesAngle;  // ÃÑ¾ËÀÇ °¢µµ
    public Color projectileColor;
}
