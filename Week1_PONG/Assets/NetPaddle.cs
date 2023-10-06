using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetPaddle : MonoBehaviourPun
{
    public float speed = 10f;

    // MonoBehaviourPun을 사용하면 PhotonView를 자동으로 가져와줌 (GetComponent 할 필요 X)
    private void Update()
    {
        // 내가 만든 photonView인지
        if (photonView.IsMine)
        {
            float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Translate(0, move, 0);
        }
    }
}
