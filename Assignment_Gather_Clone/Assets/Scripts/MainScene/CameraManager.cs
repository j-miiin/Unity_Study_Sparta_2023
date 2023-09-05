using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target;           // ī�޶� ���� ���
    public float moveSpeed;             // ī�޶� ���� �ӵ�
    private Vector3 targetPosition;     // ����� ���� ��ġ

    void Update()
    {
        if (target.gameObject != null)
        {
            // this�� ī�޶�
            // Z ���� ī�޶� ���� �״�� ����
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            // vectorA -> B���� T�� �ӵ��� �̵�
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
