using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager Instance;

    private UITitle _uiTitle;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _uiTitle = UIManager.Instance.GetUIComponent<UITitle>();
    }

    public void LoadMainScene()
    {
        PlayerPrefs.SetString(PrefsKey.PLAYER_NAME, name);
        SceneManager.LoadScene("MainScene");
    }
}
