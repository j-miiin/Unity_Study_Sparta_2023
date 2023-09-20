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

    // 플레이어 정보창 닫기 버튼에 클릭 리스너 연결
    void Start()
    {
        _closePlayerStatusButton.onClick.AddListener(CloseUI);
    }

    // player 객체를 받아와서 UI에 플레이어 상태 정보 set
    public void SetPlayerStat(PlayerDTO player)
    {
        _attackStatText.text = player.Attack.ToString();
        _shieldStatText.text = player.Shield.ToString();
        _tirednessStatText.text = player.Tiredness.ToString();
    }
}
