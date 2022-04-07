using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    // faster spawn rate and different enemies as time goes on
    // mode with survival time, time is up = win
    // spawn different enemies combination at different locations
    // 

    [SerializeField]
    private Transform _canvasTransform;

    [SerializeField]
    private EnemySpawner _enemySpawnerPrefab;

    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private GameObject _pauseUIPrefab;

    [SerializeField]
    private GameObject _retryUI;

    [SerializeField]
    private GameObject _stageClearUI;

    [SerializeField]
    private GameObject _targetPlayer;

    private PlayerInputAction _playerInput;

    private GameObject _pauseUI;

    private EnemySpawner _spawner;

    private void OnEnable()
    {
        _playerInput = new PlayerInputAction();
        _playerInput.Enable();
        _playerInput.Default.Pause.performed += OnPauseClicked;
        Player.PlayerDiedEvent += ShowRetryUI;
        TimerUI.TimerDoneEvent += TimerDone;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Default.Pause.performed -= OnPauseClicked;
        Player.PlayerDiedEvent -= ShowRetryUI;
        TimerUI.TimerDoneEvent -= TimerDone;
    }

    private void Start()
    {
        Time.timeScale = 1;

        _targetPlayer.SetActive(true);
        _spawner = Instantiate(_enemySpawnerPrefab);
        _spawner.TargetPlayer = _targetPlayer;
        _spawner.SpawnTimer = 1f;
        StartCoroutine(SpawnTimerAdjustment());

        _pauseUI = Instantiate(_pauseUIPrefab, _canvas.transform);
        _pauseUI.SetActive(false);
    }

    private void Update()
    {
        
    }

    private IEnumerator SpawnTimerAdjustment()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            _spawner.SpawnTimer /= 1.5f;
            UnityEngine.Debug.Log("Spawn time became faster");
        }
    }

    private void ShowRetryUI()
    {
        Instantiate(_retryUI, _canvasTransform);
        PauseGame();
    }

    private void OnPauseClicked(InputAction.CallbackContext context)
    {
        // move pause and resume button to a unified script
        if (_pauseUI.activeSelf == false)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void TimerDone()
    {
        Instantiate(_stageClearUI, _canvasTransform);
        Time.timeScale = 0;
        UnityEngine.Debug.Log("STAGE CLEARRRRRRRRRRRRRRRRRRRR");
        // stage clear if stage is timer type
    }

    private void PauseGame()
    {
        _pauseUI.SetActive(true);
        Time.timeScale = 0; // TODO: impelement a better way to pause the game, also fix cursor still running when game = paused
    }

    private void ResumeGame()
    {
        _pauseUI.SetActive(false);
        Time.timeScale = 1;
    }
}
