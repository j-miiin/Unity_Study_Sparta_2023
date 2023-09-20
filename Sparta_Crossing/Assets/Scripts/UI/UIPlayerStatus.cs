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
    [SerializeField] private TMP_Text _tirednessStatText;

    // �÷��̾� ����â �ݱ� ��ư�� Ŭ�� ������ ����
    void Start()
    {
        _closePlayerStatusButton.onClick.AddListener(CloseUI);
    }

    // player ��ü�� �޾ƿͼ� UI�� �÷��̾� ���� ���� set
    public void SetPlayerStat(PlayerDTO player)
    {
        _attackStatText.text = player.Attack.ToString();
        _shieldStatText.text = player.Shield.ToString();
        _tirednessStatText.text = player.Tiredness.ToString();
    }
}
