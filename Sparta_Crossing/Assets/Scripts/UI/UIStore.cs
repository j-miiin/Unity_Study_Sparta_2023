using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : GameUIClass
{
    [SerializeField] private GameObject _storeBackgroundImage;
    [SerializeField] private GameObject _storeTitleText;
    [SerializeField] private GameObject _raccoomTextBackgroundImage;
    [SerializeField] private TMP_Text _raccoonText;
    [SerializeField] private GameObject _closeStoreButton;

    private UIController _controller;

    private void Awake()
    {
        _closeStoreButton.GetComponent<Button>().onClick.AddListener(CloseUI);
    }

    private void Start()
    {
        _controller = UIManager.Instance.controller;
        _controller.OnOpenStoreEvent += OpenUI;
        base.CloseUI();
    }

    public override void OpenUI()
    {
        base.OpenUI();
        StartCoroutine(SetBackgroundFillAmount(true));
    }

    public override void CloseUI()
    {
        StartCoroutine(SetBackgroundFillAmount(false));
    }

    IEnumerator SetBackgroundFillAmount(bool isActive)
    {
        if (!isActive) SetChildObjectActive(isActive);

        float amount = _storeBackgroundImage.GetComponent<Image>().fillAmount;
        while (amount >= 0f && amount <= 1.0f)
        {
            if (isActive) amount += 0.1f;
            else amount -= 0.1f;
            _storeBackgroundImage.GetComponent<Image>().fillAmount = amount;
            yield return new WaitForFixedUpdate();
        }

        if (isActive) SetChildObjectActive(isActive);
        if (!isActive) base.CloseUI();
    }

    private void SetChildObjectActive(bool isActive)
    {
        _storeTitleText.SetActive(isActive);
        _raccoomTextBackgroundImage.SetActive(isActive);
        _closeStoreButton.SetActive(isActive);
    }
}
