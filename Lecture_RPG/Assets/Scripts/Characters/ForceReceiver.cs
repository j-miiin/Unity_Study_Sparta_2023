using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag = 0.3f;     // ����

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;

    // ������ ���� �߰����� ���� �޾��� �� �� ���� ����ϱ� ���� impact���� ����
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        // verticalVelocity�� 0���� ������ ���� �پ��ִ� ����
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        } else {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        // ���� ���� ������ SmoothDamp�� ���� ������ ���ҽ�Ŵ
        // impact�� zero�� �� ������
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void Reset()
    {
        // �������� �� ���ֱ�
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