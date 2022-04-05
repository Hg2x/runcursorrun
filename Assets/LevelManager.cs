using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject _retryUI;

    [SerializeField]
    private GameObject _targetPlayer;

    private void OnEnable()
    {
        Player.PlayerDiedEvent += ShowRetryUI;
    }

    private void OnDisable()
    {
        Player.PlayerDiedEvent -= ShowRetryUI;
    }

    private void Start()
    {
        Time.timeScale = 1;
        _targetPlayer.SetActive(true);
        var spawner = Instantiate(_enemySpawnerPrefab);
        spawner.TargetPlayer = _targetPlayer;
        Debug.Log("STARTTTTTTTTTTTTTTT");
    }

    private void ShowRetryUI()
    {
        Instantiate(_retryUI, _canvasTransform);
        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // TODO: impelement a better way to pause the game
    }
}
