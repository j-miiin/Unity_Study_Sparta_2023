using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag = 0.3f;     // 저항

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;

    // 수직의 힘과 추가적인 힘을 받았을 때 그 힘을 사용하기 위한 impact까지 받음
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        // verticalVelocity가 0보다 작으면 땅에 붙어있는 상태
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        } else {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        // 저항 값을 가지고 SmoothDamp로 값을 차근히 감소시킴
        // impact가 zero가 될 때까지
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void Reseet()
    {
        // 떨어지는 힘 없애기
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }
}
