using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� UI ������Ʈ�� ��� �޴� �θ� UI Ŭ����
// UI ������Ʈ���� ���������� ���Ǵ� ����� �޼���� ����
public class GameUIClass : MonoBehaviour
{
    public virtual void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public virtual void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
