using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 UI 컴포넌트가 상속 받는 부모 UI 클래스
// UI 컴포넌트에서 공통적으로 사용되는 기능을 메서드로 정의
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
