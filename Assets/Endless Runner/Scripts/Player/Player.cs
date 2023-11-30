using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _changePosDuration;
    [SerializeField] private float _step;

    [SerializeField] private SmoothHealthBar _healthBar;
    [SerializeField] private float _maxHealth;

    private float _leftBound;
    private float _rightBound;

    private Health _health;
    private PlayerMover _mover;

    private bool _isInitialized = false;

    public Action OnDeath;

    public bool IsAlive => _health.IsAlive;

    private void OnEnable()
    {
        if (_isInitialized == true)
        {
            OnDeath += Die;
            _health.OnDeath += OnDeath;
        }
    }

    private void OnDisable()
    {
        OnDeath -= Die;
        _health.OnDeath -= OnDeath;
    }

    private void Update()
    {
        if (!_isInitialized)
        {
            return;
        }

        _mover.HandleInput();
    }

    public void Init(float leftBound, float rightBound)
    {
        _leftBound = leftBound;
        _rightBound = rightBound;
        _mover = new PlayerMover(this, _changePosDuration, _leftBound, _rightBound, _step);
        _health = new Health(_maxHealth);

        OnDeath += Die;
        _health.OnDeath += OnDeath;

        _healthBar.Init(_health);

        _isInitialized = true;
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    public void Heal(float healAmount)
    {
        _health.Heal(healAmount);
    }

    private void Die()
    {
        _isInitialized = false;
        gameObject.SetActive(false);
    }
}
