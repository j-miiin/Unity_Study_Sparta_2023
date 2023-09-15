using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private int speed = 4;

    private TopDownCharacterController _controller;
    private InputAction _moveAction;

    private Vector2 _movementDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;

    private Animator _animator;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
        _moveAction = GetComponent<PlayerInput>().actions.FindAction("Move");
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Start()
    {
        _controller.OnMoveEvent += Move;
        _moveAction.started += context => { Debug.Log(_animator.GetInteger("isWalking")); };
        _moveAction.performed += context =>
        {
            if (Input.GetKey(KeyCode.S))
            {
                _animator.SetInteger("isWalking", 2);
            } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                _animator.SetInteger("isWalking", 1);
            } else if (Input.GetKey(KeyCode.W))
            {
                _animator.SetInteger("isWalking", 3);
            }
        };
        _moveAction.canceled += context => { _animator.SetInteger("isWalking", 0); };
    }

    private void FixedUpdate()
    {
        ApplyMovement(_movementDirection);
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction *= speed;
        _rigidbody.velocity = direction;
    }
}
