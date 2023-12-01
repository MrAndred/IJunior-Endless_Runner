using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnParent;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<Enemy> _enemyTemplates;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _enemyPoolCount;

    private Coroutine _spawning;
    private EnemyPool _enemyPool;

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
        _isSpawning = true;
        _enemyPool = new EnemyPool(_enemyPoolCount, _enemyTemplates, _spawnParent);
        _spawning = StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        WaitForSeconds spawnDelay = new WaitForSeconds(_spawnDelay);

        while (_isSpawning == true)
        {
            Enemy enemy = _enemyPool.GetEnemy();
            enemy.Init();
            enemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
            enemy.gameObject.SetActive(true);

            yield return spawnDelay;
        }
    }
}
