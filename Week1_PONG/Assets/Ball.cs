using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviourPun, IPunObservable
{
    public float speed;
    public Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // 호스트가 아닐 때는 Launch를 할 필요가 없음
        // 호스트가 움직이는 공의 동작을 동기화만 하면 됨
        if (!photonView.AmOwner)
        {
            return;
        }

        Launch();
    }

    private void Launch()
    {
        if (!photonView.AmOwner)
        {
            return;
        }

        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        rigidbody.velocity = new Vector2(x* speed, y* speed);
    }

    public void Reset()
    {
        rigidbody.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        Invoke("Launch", 1);
    }

    // 원하는 방법으로 동기화하고 싶을 때 IPunObservable을 사용
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 호스트일 때 정보 보내기
        if (stream.IsWriting)
        {
            stream.SendNext(rigidbody.position);
            stream.SendNext(rigidbody.velocity);
        }
        // 아닐 때는 정보 읽기
        // 정보를 보낸 순서대로 읽어야 함
        else
        {
            rigidbody.position = (Vector2)stream.ReceiveNext();
            rigidbody.velocity = (Vector2)stream.ReceiveNext();
        }
    }
}
