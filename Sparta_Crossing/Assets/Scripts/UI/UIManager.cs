using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UIController controller;

    Dictionary<string, GameUIClass> uiDic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        controller = GetComponent<UIController>();
        uiDic = new Dictionary<string, GameUIClass>();
    }

    // 요청 받은 UI 컴포넌트가 딕셔너리 안에 있으면 반환
    // 없으면 Load 해와서 저장 후에 반환
    public T GetUIComponent<T>() where T : GameUIClass
    {
        string key = typeof(T).Name;
        if (!uiDic.ContainsKey(key))
        {
            var obj = Instantiate(Resources.Load($"Prefabs/{key}"));
            uiDic.Add(key, obj.GetComponent<T>());
        }
        return (T)uiDic[key];
    }

    public void RemoveComponent<T>(T uiComponent) where T : GameUIClass
    {
        if (uiDic.ContainsKey(typeof(T).Name))
        {
            uiDic.Remove(typeof(T).Name);
        }
    }
}
