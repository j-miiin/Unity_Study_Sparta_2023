using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    public static SingletonManager Instance;

    private static UIManager UIManagerInstance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        InitUIManagerInstance();
    }

    private void InitUIManagerInstance()
    {
        if (UIManagerInstance == null)
        {
            var obj = new GameObject();
            UIManagerInstance = obj.AddComponent<UIManager>();
        }
    }
}
