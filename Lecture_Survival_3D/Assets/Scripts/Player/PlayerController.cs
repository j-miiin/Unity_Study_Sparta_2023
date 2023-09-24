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
    private float camCurXRot;       // 카메라 X Rotation
    public float lookSensitivity;   // 민감도

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
        Cursor.lockState = CursorLockMode.Locked;   // 커서가 안 보이도록

    }

    // 물리적인 처리
    private void FixedUpdate()
    {
        Move();
    }

    // 모든 처리가 끝난 후 Update가 동작 -> 카메라 작업에 주로 사용
    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    // curMoveInput으로 이동 처리
    private void Move()
    {
        // 캐릭터가 서있는 상태에서의 forwar와 right에 입력값을 더해줌
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;  // y 값은 없애야 하는데, velocity의 y 값으로 가져옴 (?)

        _rigidbody.velocity = dir;
    }


    // 마우스로 카메라 이동
    void CameraLook()
    {
        // mouse가 위, 아래로 움직임 (y값 변화) 
        // rotation은 x가 변화 -> 가로축(x축)이 회전할 때 고개가 끄덕끄덕 하기 때문
        camCurXRot += mouseDelta.y * lookSensitivity;   
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);   // min과 max 사이에 가둠
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);  // 마우스 이동에 따라 움직이도록

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); 
    }

    // 마우스 이동 입력을 받음
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();  // 입력값을 Vector2로 읽어옴
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // Started : 처음 눌릴 때 / Performed : 눌리고 있을 때 / Canceled : 입력이 종료될 때
        if (context.phase == InputActionPhase.Performed)  
        {
            curMovementInput = context.ReadValue<Vector2>();   
        } else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;    // 입력이 끝나면 움직이면 안 되므로
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // 플레이어가 땅을 밟고 있는지 체크하지 않으면 무한으로 점프하게 됨
            if (IsGrounded())   
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);     // Impulse는 질량을 갖고 처리
        }
    }

    // 플레이어가 땅을 밟고 있는지 확인
    private bool IsGrounded()
    {
        // 지형이 울퉁불퉁하기 때문에 여러 방향으로 확인
        // 플레이어의 위에서 정수리를 바라봤을 때 머리 기준 상, 하, 좌, 우 방향 바닥을 다 쏴서 체크
        Ray[] rays = new Ray[4]
        {
            // 조금만 더 위에서 ray를 쏘기 위해 Vector3.up * 0.01f만큼 더해줌
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {   
            // 4개 중 하나라도 땅에 닿으면 점프를 하지 않음
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    // 복잡한 오브젝트들이 있는 상태에서 Gizmo를 봐야 할 때 사용
    // 선택했을 때만 Gizmo가 보이게 만들거나 항상 보이게 할 수도 있음
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
