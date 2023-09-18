using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownEnemyController : TopDownCharacterController
{
    GameManager gameManager;
    protected Transform ClosestTarget { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        gameManager = GameManager.instance;
        ClosestTarget = gameManager.Player;
    }

    protected virtual void FixedUpdate()
    {

    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }

    protected Vector2 DirectionToTarget()
    {
        // 현재 오브젝트에서 ClosetTarget을 바라보는 방향
        return (ClosestTarget.position - transform.position).normalized;
    }
}
