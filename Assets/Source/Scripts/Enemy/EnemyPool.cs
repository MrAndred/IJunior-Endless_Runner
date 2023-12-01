using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    private Transform _parent;
    private List<Enemy> _templates;
    private List<Enemy> _enemies = new List<Enemy>();

    public EnemyPool(float enemiesCount, List<Enemy> templates, Transform parent)
    {
        _parent = parent;
        _templates = templates;

        for (int i = 0; i < enemiesCount; i++)
        {
            Enemy enemy = GameObject.Instantiate(_templates[Random.Range(0, _templates.Count)], _parent);
            enemy.Init();
            enemy.gameObject.SetActive(false);

            _enemies.Add(enemy);
        }
    }

    public Enemy GetEnemy()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].gameObject.activeSelf == false)
            {
                return _enemies[i];
            }
        }

        Enemy enemy = GameObject.Instantiate(_templates[Random.Range(0, _templates.Count)], _parent);
        enemy.Init();
        _enemies.Add(enemy);

        return enemy;
    }
}
