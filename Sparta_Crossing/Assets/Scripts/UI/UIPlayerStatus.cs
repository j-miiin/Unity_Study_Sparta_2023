using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatus : GameUIClass
{

    [SerializeField] private Button _closePlayerStatusButton;
    [SerializeField] private TMP_Text _attackStatText;
    [SerializeField] private TMP_Text _shieldStatText;
    [SerializeField] private TMP_Text _healthStatText;

    void Start()
    {
        _closePlayerStatusButton.onClick.AddListener(CloseUI);
    }

    public void SetPlayerStat(int attackStat, int shieldStat, int healthStat)
    {
        _attackStatText.text = attackStat.ToString();
        _shieldStatText.text = shieldStat.ToString();
        _healthStatText.text = healthStat.ToString();
    }
}
