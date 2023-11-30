using UnityEngine;

public class EnemyMover
{
    private Enemy _enemy;
    private float _speed;

    public EnemyMover(Enemy enemy, float speed)
    {
        _enemy = enemy;
        _speed = speed;
    }

    public void Move()
    {
        _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _enemy.transform.position + Vector3.down, _speed * Time.deltaTime);
    }
}
