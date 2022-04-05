using System.Collections;
using System.Collections.Generic;
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
    private GameObject _targetPlayer;

    private PlayerInputAction _playerInput;

    private GameObject _pauseUI;

    private void OnEnable()
    {
        _playerInput = new PlayerInputAction();
        _playerInput.Enable();
        _playerInput.Default.Pause.performed += OnPauseClicked;
        Player.PlayerDiedEvent += ShowRetryUI;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Default.Pause.performed -= OnPauseClicked;
        Player.PlayerDiedEvent -= ShowRetryUI;
    }

    private void Start()
    {
        Time.timeScale = 1;

        _targetPlayer.SetActive(true);
        var spawner = Instantiate(_enemySpawnerPrefab);
        spawner.TargetPlayer = _targetPlayer;
        spawner.SpawnTimer = 1f; // TODO: change later to scale with time

        _pauseUI = Instantiate(_pauseUIPrefab, _canvas.transform);
        _pauseUI.SetActive(false);
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

    private void PauseGame()
    {
        _pauseUI.SetActive(true);
        Time.timeScale = 0; // TODO: impelement a better way to pause the game
    }

    private void ResumeGame()
    {
        _pauseUI.SetActive(false);
        Time.timeScale = 1;
    }
}
