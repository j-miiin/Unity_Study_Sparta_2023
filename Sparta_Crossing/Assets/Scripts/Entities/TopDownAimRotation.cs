using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;
    [SerializeField] private SpriteRenderer characterRenderer;

    private TopDownCharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    public void OnAim(Vector2 newAimDirection)
    {
        RotateArm(newAimDirection);
    }

    private void RotateArm(Vector2 direction)
    {
        // x, y에 대하여 tan 값을 구함->벡터의 각도
        // Mathf.Rad2Deg를 곱해서 radian 값을 degree 값으로 변환 -> Euler 함수 사용 가능
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 캐릭터를 기준으로 활 각도의 절댓값이 90을 넘어가면 활을 뒤집음 -> 캐릭터의 방향도 뒤집음
        armRenderer.flipY = Mathf.Abs(rotZ) > 90f;
        characterRenderer.flipX = armRenderer.flipY;
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);   // 실제로 무기 회전
    }   
}
