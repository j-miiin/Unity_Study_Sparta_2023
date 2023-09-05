using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TMP_Text clockText;

    private void Update()
    {
        string time = DateTime.Now.ToString("t");
        clockText.text = time;
    }
}
