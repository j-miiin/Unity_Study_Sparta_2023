using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITitle : GameUIClass
{
    [SerializeField] private GameObject _titleImage;
    [SerializeField] private GameObject _startText;
    [SerializeField] private GameObject _userNameInputFieldImage;
    [SerializeField] private Graphic _placeholder;
    [SerializeField] private GameObject _okButton;

    private void Awake()
    {
        _startText.GetComponent<Button>().onClick.AddListener(MakeTransition);
        _okButton.GetComponent<Button>().onClick.AddListener(LoadMainScene);
    }

    private void Start()
    {
        _placeholder.GetComponent<TMP_Text>().text = "닉네임을 입력하세요";
    }

    private void MakeTransition()
    {
        Instantiate(Resources.Load<GameObject>("Animation/msVFX_Stylized Smoke 1"));
        _titleImage.SetActive(false);
        _startText.SetActive(false);
        Invoke("OpenUserNameInputField", 0.4f);
    }

    private void OpenUserNameInputField()
    {
        _userNameInputFieldImage.SetActive(true);
        _okButton.SetActive(true);
    }

    private void LoadMainScene()
    {
        string name = _userNameInputFieldImage.GetComponent<TMP_InputField>().text;
        if (name.Length >= 2 && name.Length <= 10)
        {
            StartSceneManager.Instance.LoadMainScene();
        } else
        {
            _userNameInputFieldImage.GetComponent<TMP_InputField>().text = "";
            _placeholder.GetComponent<TMP_Text>().text = "2~10 글자 이내로 입력하세요";
        }
    }
}
