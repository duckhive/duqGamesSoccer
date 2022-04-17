using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TMP_Text _timerText;

    private void Awake()
    {
        _timerText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        DisplayTimer();
    }

    private void DisplayTimer()
    {
        if (GameManager.Instance.gameActive)
        {
            if (GameManager.Instance.timer > 0)
                GameManager.Instance.timer -= Time.deltaTime;

            if (GameManager.Instance.timer < 0)
                GameManager.Instance.timer = 0;

            float minutes = Mathf.FloorToInt(GameManager.Instance.timer / 60);
            float seconds = Mathf.FloorToInt(GameManager.Instance.timer % 60);

            _timerText.text = $"{minutes:0}:{seconds:00}";
        }
    }
}
