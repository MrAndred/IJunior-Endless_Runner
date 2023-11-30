using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnParent;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Enemy[] _enemyTemplates;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _enemyPoolCount;

    private Coroutine _spawning;

    private List<Enemy> _enemies = new List<Enemy>();
    private bool _isSpawning = false;
    private bool _isInitialized = false;

    private void OnEnable()
    {
        if (_isInitialized)
        {
            _spawning = StartCoroutine(SpawnEnemy());
            _isSpawning = true;
        }
    }

    private void OnDisable()
    {
        if (_isInitialized)
        {
            _isSpawning = false;
            StopCoroutine(_spawning);
        }
    }

    public void Init()
    {
        for (int i = 0; i < _enemyPoolCount; i++)
        {
            Enemy enemy = Instantiate(_enemyTemplates[Random.Range(0, _enemyTemplates.Length)], _spawnParent);
            enemy.Init();
            enemy.gameObject.SetActive(false);

            _enemies.Add(enemy);
        }

        _isSpawning = true;
        _spawning = StartCoroutine(SpawnEnemy());
    }

    private Enemy GetEnemy()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].gameObject.activeSelf == false)
            {
                return _enemies[i];
            }
        }

        Enemy enemy = Instantiate(_enemyTemplates[Random.Range(0, _enemyTemplates.Length)], transform);
        enemy.Init();
        _enemies.Add(enemy);

        return enemy;
    }

    private IEnumerator SpawnEnemy()
    {
        WaitForSeconds spawnDelay = new WaitForSeconds(_spawnDelay);

        while (_isSpawning == true)
        {
            Enemy enemy = GetEnemy();
            enemy.Init();
            enemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
            enemy.gameObject.SetActive(true);

            yield return spawnDelay;
        }
    }
}
