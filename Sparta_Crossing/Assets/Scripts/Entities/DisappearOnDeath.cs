using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnDeath : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _healthSystem.OnDeath += OnDeath;
    }

    void OnDeath()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        // 모든 컴포넌트는 Behaviour를 상속받게 됨
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}
