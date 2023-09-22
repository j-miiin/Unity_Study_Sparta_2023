using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    public static SingletonManager Instance;

    Dictionary<string, MonoBehaviour> managerDic;

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

        // 닉네임 입력부터 하고 싶을 때 PlayerDatabase 지우고 주석 풀기
        PlayerPrefs.DeleteAll();

        managerDic = new Dictionary<string, MonoBehaviour>();

        InitManagerInstance<DataManager>();
        InitManagerInstance<UIManager>();
        InitManagerInstance<GameManager>();
        InitManagerInstance<SoundManager>();
    }

    public void InitManagerInstance<T>() where T : MonoBehaviour
    {
        string key = typeof(T).Name;
        if (!managerDic.ContainsKey(key))
        {
            var obj = new GameObject();
            obj.name = key;
            managerDic.Add(key, obj.AddComponent<T>());
        }
    }
}
