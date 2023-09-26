using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangedAttackData _attackData;
    private float _currentDuration;     // ���� �ð�
    private Vector2 _direction;
    private bool _isReady;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trailRenderer;
    private ProjectileManager _projectileManager;

    public bool fxOnDestroy = true;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // ������ ������ ���� ������Ʈ���� �˻�
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (!_isReady)
        {
            return;
        }

        _currentDuration += Time.deltaTime;  // ���� �ð� ����

        if (_currentDuration > _attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        // ������� �ʾҴٸ� �̵�
        _rigidbody.velocity = _direction * _attackData.speed;   // ����ü �ӵ�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            // trigger �浹�� �浹ü�� �޾ƿ���, collision �浹�� ���� �浹�� �޾ƿ�
            // ���� �ε����� ���� �������� ��������
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f, fxOnDestroy);
        } else if (_attackData.target.value == (_attackData.target.value | (1 << collision.gameObject.layer)))
        {
            // ���Ÿ� ������ �浹���� ��
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

        UpdateProjectileSprite();   // ������ ����
            
        _trailRenderer.Clear();
        _currentDuration = 0;
        _spriteRenderer.color = attackData.projectileColor;

        // ���� ȸ���ϰ� �ִ��� ������ ����, ������, ��, �ڰ� ����
        // �츮�� 2D�̹Ƿ� x�� ������ direction���� ���߸� direction �������� ȸ���ϰ� ��
        transform.right = _direction;
        _isReady = true;
    }

    private void UpdateProjectileSprite()
    {
        // ���� �͵��� ������ data ���� size ���� ���� ũ�⸦ ������ �� ����
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
