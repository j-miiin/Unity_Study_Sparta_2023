using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;     
    private Vector2 curMovementInput;
    public float jumpForce;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;       // ī�޶� X Rotation
    public float lookSensitivity;   // �ΰ���

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody _rigidbody;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // Ŀ���� �� ���̵���

    }

    // �������� ó��
    private void FixedUpdate()
    {
        Move();
    }

    // ��� ó���� ���� �� Update�� ���� -> ī�޶� �۾��� �ַ� ���
    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    // curMoveInput���� �̵� ó��
    private void Move()
    {
        // ĳ���Ͱ� ���ִ� ���¿����� forwar�� right�� �Է°��� ������
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;  // y ���� ���־� �ϴµ�, velocity�� y ������ ������ (?)

        _rigidbody.velocity = dir;
    }


    // ���콺�� ī�޶� �̵�
    void CameraLook()
    {
        // mouse�� ��, �Ʒ��� ������ (y�� ��ȭ) 
        // rotation�� x�� ��ȭ -> ������(x��)�� ȸ���� �� ���� �������� �ϱ� ����
        camCurXRot += mouseDelta.y * lookSensitivity;   
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);   // min�� max ���̿� ����
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);  // ���콺 �̵��� ���� �����̵���

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); 
    }

    // ���콺 �̵� �Է��� ����
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();  // �Է°��� Vector2�� �о��
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // Started : ó�� ���� �� / Performed : ������ ���� �� / Canceled : �Է��� ����� ��
        if (context.phase == InputActionPhase.Performed)  
        {
            curMovementInput = context.ReadValue<Vector2>();   
        } else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;    // �Է��� ������ �����̸� �� �ǹǷ�
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // �÷��̾ ���� ��� �ִ��� üũ���� ������ �������� �����ϰ� ��
            if (IsGrounded())   
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);     // Impulse�� ������ ���� ó��
        }
    }

    // �÷��̾ ���� ��� �ִ��� Ȯ��
    private bool IsGrounded()
    {
        // ������ ���������ϱ� ������ ���� �������� Ȯ��
        // �÷��̾��� ������ �������� �ٶ���� �� �Ӹ� ���� ��, ��, ��, �� ���� �ٴ��� �� ���� üũ
        Ray[] rays = new Ray[4]
        {
            // ���ݸ� �� ������ ray�� ��� ���� Vector3.up * 0.01f��ŭ ������
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {   
            // 4�� �� �ϳ��� ���� ������ ������ ���� ����
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    // ������ ������Ʈ���� �ִ� ���¿��� Gizmo�� ���� �� �� ���
    // �������� ���� Gizmo�� ���̰� ����ų� �׻� ���̰� �� ���� ����
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
