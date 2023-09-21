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
    [SerializeField] private TMP_InputField _userNameInputField;
    [SerializeField] private Graphic _placeholder;
    [SerializeField] private GameObject _okButton;

    private void Awake()
    {
        _startText.GetComponent<Button>().onClick.AddListener(MakeTransition);
        _okButton.GetComponent<Button>().onClick.AddListener(() => { LoadMainScene(true); });
    }

    private void Start()
    {
        _placeholder.GetComponent<TMP_Text>().text = "�г����� �Է��ϼ���";
    }

    private void MakeTransition()
    {
        if (PlayerPrefs.HasKey(PrefsKey.PLAYER_NAME)) LoadMainScene(false);
        else
        {
            Instantiate(Resources.Load<GameObject>("Animation/msVFX_Stylized Smoke 1"));
            _titleImage.SetActive(false);
            _startText.SetActive(false);
            Invoke("OpenUserNameInputField", 0.4f);
        }
    }

    private void OpenUserNameInputField()
    {
        _userNameInputFieldImage.SetActive(true);
        _okButton.SetActive(true);
    }

    private void LoadMainScene(bool isNewPlayer)
    {
        // ���ο� �÷��̾�� �г����� �Է¹ް� ������ ������ ���
        if (isNewPlayer)
        {
            string playerName = _userNameInputField.GetComponent<TMP_InputField>().text;
            if (playerName.Length >= 2 && playerName.Length <= 10)
            {
                GameManager.Instance.LoadMainScene(playerName);
            }
            else
            {
                _userNameInputField.GetComponent<TMP_InputField>().text = "";
                _placeholder.GetComponent<TMP_Text>().text = "2~10 ���� �̳��� �Է��ϼ���";
            }
        } 
        // �̹� �����ϴ� �÷��̾ ������ �������� ���
        else
        {
            GameManager.Instance.LoadMainScene("");
        }
        
    }
}
