using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{ // TODO: maybe use object pooling here
    [SerializeField]
    private GameObject[] _spawnBorder; // point 1,2,3,4. Has to be in taht order

    public GameObject TargetPlayer{ get { return _targetPlayer; } set{ _targetPlayer = value; } }
    private GameObject _targetPlayer;

    public float SpawnTimer { get { return _spawnTimer; } set { _spawnTimer = value; } }
    private float _spawnTimer;

    [SerializeField]
    private GameObject[] _enemyToSpawn; // maybe change to list

    private List<Vector3> _spawnBorderCoordinates;

    private (float minX, float maxX) _outerXBorder;
    private  (float minX, float maxX) _innerXBorder;
    private  (float minY, float maxY) _outerYBorder;
    private  (float minY, float maxY) _innerYBorder;

    private Vector3 _spawnPosition;

    private void Start()
    {
        gameObject.SetActive(true);
        SetBorder();
        // TODO: fix invoke not spawning when re entering the game from main menu
        StartCoroutine(SpawnEnemyCoroutine());
        if (_targetPlayer != null)
        {
            Debug.Log("palyer not null");
        }
    }

    private void SetBorder()
    {
        long seed = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        Random.InitState((int)seed);

        _spawnBorderCoordinates = new List<Vector3>();

        foreach (var go in _spawnBorder)
        {
            _spawnBorderCoordinates.Add(go.transform.position);
        }

        _outerXBorder = (_spawnBorderCoordinates[0].x, _spawnBorderCoordinates[3].x);
        _innerXBorder = (_spawnBorderCoordinates[1].x, _spawnBorderCoordinates[2].x);
        _outerYBorder = (_spawnBorderCoordinates[3].y, _spawnBorderCoordinates[0].y);
        _innerYBorder = (_spawnBorderCoordinates[2].y, _spawnBorderCoordinates[1].y);
    }

    private void SpawnEnemy(GameObject enemyGameObject)
    {
        _spawnPosition = GenerateSpawnLocation();
        var enemy = Instantiate(enemyGameObject, _spawnPosition, Quaternion.identity);
        enemy.transform.SetParent(transform);
        //Debug.Log(_spawnPosition);

        if (_targetPlayer != null)
        {
            var direction = _targetPlayer.transform.position - enemy.transform.position;
            direction.Normalize();
            enemy.GetComponent<EnemyBase>().Direction = direction;
            //Debug.Log(direction + "direction");
            //transform.Rotate(0f, 0f, Random.Range(0.0f, 360.0f)); // find a better way to rotate without fucking up walking direction
        }
        else
        {
            // TODO: fix this part of code
            StopCoroutine(SpawnEnemyCoroutine());
            gameObject.SetActive(false);
        }
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            SpawnEnemy(_enemyToSpawn[Random.Range(0, _enemyToSpawn.Length)]);
            yield return new WaitForSeconds(_spawnTimer);
        }
    }

    private Vector3 GenerateSpawnLocation()
    {
        // TODO: Optimize this
        float xPos, yPos;
        int firstFlip = Random.Range(0, 2);   // creates a number between 0 and 1
        if (firstFlip == 0)
        {
            xPos = Random.Range(_outerXBorder.minX, _outerXBorder.maxX);

            int secondFlip = Random.Range(0, 2);
            if (secondFlip == 0)
            {
                yPos = Random.Range(_outerYBorder.minY, _innerYBorder.minY);
            }
            else
            {
                yPos = Random.Range(_outerYBorder.maxY, _innerYBorder.maxY);
            }
        }
        else
        {
            yPos = Random.Range(_outerYBorder.minY, _outerYBorder.minY);

            int secondFlip = Random.Range(0, 2);
            if (secondFlip == 0)
            {
                xPos = Random.Range(_outerXBorder.minX, _innerXBorder.minX);
            }
            else
            {
                xPos = Random.Range(_outerXBorder.maxX, _innerXBorder.maxX);
            }
        }
        return new Vector3(xPos, yPos, 0f);
    }

    private void Update()
    {
        // make a coroutine to instantiate enemyToSpawn in x seconds
    }
}
