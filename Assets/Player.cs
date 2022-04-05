using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event PlayerDied PlayerDiedEvent;
    public static event SendPlayerStats SendPlayerStatsEvent;

    [SerializeField]
    private int _playerHealth = 10;

    private Camera _camera;

    public UnitType Type { get { return _type; } }
    private UnitType _type = UnitType.Player;

    private void OnEnable()
    {
        EnemyBase.DamageToPlayer += TakeDamage;
    }

    private void OnDisable()
    {
        EnemyBase.DamageToPlayer -= TakeDamage;
    }

    private void Start()
    {
        _camera = Camera.main; // TODO: maybe change this later
        SendPlayerStatsEvent?.Invoke(_playerHealth);
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }

    private void TakeDamage(int damage)
    {
        _playerHealth -= damage;
        SendPlayerStatsEvent?.Invoke(_playerHealth);
        Debug.Log(damage + " damage taken");
        if (_playerHealth <= 0)
        {
            PlayerDiedEvent?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
