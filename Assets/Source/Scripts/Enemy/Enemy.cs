using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private EnemyMover _mover;
    private EnemyAttacker _attacker;
    private bool _isInitialized = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (player.IsAlive == true)
            {
                _attacker.Attack(player);
            }
        }

        Die();
    }

    private void Update()
    {
        if (_isInitialized)
        {
            _mover.Move();
        }
    }

    public void Init()
    {
        _attacker = new EnemyAttacker(_damage);
        _mover = new EnemyMover(this, _speed);
        _isInitialized = true;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        _isInitialized = false;
    }
}
