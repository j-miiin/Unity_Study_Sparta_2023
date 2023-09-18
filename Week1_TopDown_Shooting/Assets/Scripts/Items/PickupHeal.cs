using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHeal : PickupItem
{
    [SerializeField] private int _healValue = 10;
    private HealthSystem _healSystem;

    protected override void OnPickedUp(GameObject receiver)
    {
        _healSystem = receiver.GetComponent<HealthSystem>();
        _healSystem.ChangeHealth(_healValue);
    }
}
