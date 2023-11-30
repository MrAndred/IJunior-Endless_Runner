using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKitSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnParent;
    [SerializeField] private AidKit[] _aidKitPrefabs;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _aidKitPoolCount;

    [SerializeField] private float _spawnChance;

    private float _leftSpawnBorder;
    private float _rightSpawnBorder;

    private float _spawnHeight;

    private List<AidKit> _aidKits = new List<AidKit>();
    private float _minSpawnChance = 0;
    private float _maxSpawnChance = 100;
    private bool _isSpawning = false;
    private bool _isInitialized = false;

    private Coroutine _spawning;

    private void OnEnable()
    {
        if (_isInitialized == false)
        {
            Init(_spawnHeight, _leftSpawnBorder, _rightSpawnBorder);
        }
    }

    private void OnDisable()
    {
        _isInitialized = false;
        _isSpawning = false;
        StopCoroutine(_spawning);
    }

    public void Init(float spawnHeight, float leftSpawnBorder, float rightSpawnBorder)
    {
        _leftSpawnBorder = leftSpawnBorder;
        _rightSpawnBorder = rightSpawnBorder;
        _spawnHeight = spawnHeight;

        for (int i = 0; i < _aidKitPoolCount; i++)
        {
            AidKit aidKit = Instantiate(_aidKitPrefabs[Random.Range(0, _aidKitPrefabs.Length)], transform);
            _aidKits.Add(aidKit);
            aidKit.gameObject.SetActive(false);
        }

        _isSpawning = true;
        _spawning = StartCoroutine(SpawnAidKitByPeriod());
    }

    private IEnumerator SpawnAidKitByPeriod()
    {
        WaitForSeconds spawnDelay = new WaitForSeconds(_spawnDelay);

        while (_isSpawning)
        {
            SpawnAidKit();
            yield return spawnDelay;
        }
    }

    private void SpawnAidKit()
    {
        if (Random.Range(_minSpawnChance, _maxSpawnChance) < _spawnChance)
        {
            AidKit aidKit = GetAidKit();
            aidKit.transform.position = new Vector2(Random.Range(_leftSpawnBorder, _rightSpawnBorder), _spawnHeight);
            aidKit.gameObject.SetActive(true);
        }
    }

    private AidKit GetAidKit()
    {
        for (int i = 0; i < _aidKits.Count; i++)
        {
            if (_aidKits[i].gameObject.activeSelf == false)
            {
                return _aidKits[i];
            }
        }

        return null;
    }
}
