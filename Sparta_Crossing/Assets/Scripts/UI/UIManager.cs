using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    Dictionary<string, GameUIClass> uiDic;

    private void Awake()
    {
        Instance = this;
        uiDic = new Dictionary<string, GameUIClass>();
        
    }

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
}
