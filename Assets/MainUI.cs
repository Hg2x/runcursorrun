using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _healthText;

    private void Start()
    {
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
