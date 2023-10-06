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
        // ȣ��Ʈ�� �ƴ� ���� Launch�� �� �ʿ䰡 ����
        // ȣ��Ʈ�� �����̴� ���� ������ ����ȭ�� �ϸ� ��
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

    // ���ϴ� ������� ����ȭ�ϰ� ���� �� IPunObservable�� ���
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ȣ��Ʈ�� �� ���� ������
        if (stream.IsWriting)
        {
            stream.SendNext(rigidbody.position);
            stream.SendNext(rigidbody.velocity);
        }
        // �ƴ� ���� ���� �б�
        // ������ ���� ������� �о�� ��
        else
        {
            rigidbody.position = (Vector2)stream.ReceiveNext();
            rigidbody.velocity = (Vector2)stream.ReceiveNext();
        }
    }
}
