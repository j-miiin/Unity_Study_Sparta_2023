using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAreacController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("�Ɽ");
            GameObject canvas = GameObject.FindWithTag("Canvas");

            if (canvas == null) return;

            canvas.transform.Find("CallNpcDialog").gameObject.SetActive(true);
        }
    }
}
