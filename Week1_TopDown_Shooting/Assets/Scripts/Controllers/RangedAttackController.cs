using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangedAttackData _attackData;
    private float _currentDuration;     // 실행 시간
    private Vector2 _direction;
    private bool _isReady;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trailRenderer;
    private ProjectileManager _projectileManager;

    public bool fxOnDestroy = true;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // 나에게 없으면 하위 오브젝트까지 검사
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (!_isReady)
        {
            return;
        }

        _currentDuration += Time.deltaTime;  // 실제 시간 누적

        if (_currentDuration > _attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        // 사라지지 않았다면 이동
        _rigidbody.velocity = _direction * _attackData.speed;   // 투사체 속도
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            // trigger 충돌은 충돌체만 받아오고, collision 충돌은 실제 충돌을 받아옴
            // 벽에 부딪히고 조금 안쪽으로 들어오도록
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f, fxOnDestroy);
        } else if (_attackData.target.value == (_attackData.target.value | (1 << collision.gameObject.layer)))
        {
            // 원거리 공격이 충돌했을 때
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-_attackData.power);
                if (_attackData.isOnKnockback)
                {
                    TopDownMovement movement = collision.GetComponent<TopDownMovement>();
                    if (movement != null)
                    {
                        movement.ApplyKnockback(transform, _attackData.knockbackPower, _attackData.knockbackTime);
                    }
                }
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

    public void InitializeAttack(Vector2 direction, RangedAttackData attackData, ProjectileManager projectileManager)
    {
        _projectileManager = projectileManager;
        _attackData = attackData;
        _direction = direction;

        UpdateProjectileSprite();   // 사이즈 조절
            
        _trailRenderer.Clear();
        _currentDuration = 0;
        _spriteRenderer.color = attackData.projectileColor;

        // 지금 회전하고 있더라도 원래의 왼쪽, 오른쪽, 앞, 뒤가 있음
        // 우리는 2D이므로 x축 방향을 direction으로 맞추면 direction 방향으로 회전하게 됨
        transform.right = _direction;
        _isReady = true;
    }

    private void UpdateProjectileSprite()
    {
        // 같은 것들을 쓰더라도 data 안의 size 값에 따라 크기를 조절할 수 있음
        transform.localScale = Vector3.one * _attackData.size;
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            _projectileManager.CreateImpactParticlesAtPosition(position, _attackData);
        }
        gameObject.SetActive(false);
    }
}
