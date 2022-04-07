using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainUI : MonoBehaviour
{
    // TODO: maybe use a level/stage class to transfer all data
    [SerializeField]
    private TextMeshProUGUI _healthText;

    [SerializeField]
    private TimerUI _timerUI;

    public void StartTimer()
    {
        int minutes = 0; // from LevelManager
        int seconds = 10;
        _timerUI.StartTimer(minutes, seconds);
    }

    private void Start()
    {
        StartTimer();
    }

    private void OnEnable()
    {
        Player.SendPlayerStatsEvent += SetPlayerHealthText;
    }

    private void OnDisable()
    {
        Player.SendPlayerStatsEvent += SetPlayerHealthText;
    }

    private void SetPlayerHealthText(int playerHealth)
    {
        _healthText.text = "Health: " + playerHealth;
    }
}
