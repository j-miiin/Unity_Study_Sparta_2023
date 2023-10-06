using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetPaddle : MonoBehaviourPun
{
    public float speed = 10f;

    // MonoBehaviourPun�� ����ϸ� PhotonView�� �ڵ����� �������� (GetComponent �� �ʿ� X)
    private void Update()
    {
        // ���� ���� photonView����
        if (photonView.IsMine)
        {
            float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Translate(0, move, 0);
        }
    }
}
