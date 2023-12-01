using System;

public class Health
{
    public Action OnHealthChanged;
    public Action OnDeath;

    private float _health;
    private bool _isAlive = true;

    private float _maxHealth;
    private float _minHealth = 0;

    public Health(float health)
    {
        _health = health;
        _maxHealth = health;
    }

    public float Value => _health;

    public bool IsAlive => _isAlive;

    public void Heal(float health)
    {
        if (_health > _maxHealth)
        {
            return;
        }

        if (_isAlive == false || health < 0)
        {
            return;
        }

        if (_health + health > _maxHealth)
        {
            _health = _maxHealth;
        }
        else
        {
            _health += health;
        }

        OnHealthChanged?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (_isAlive == false || damage < 0)
        {
            return;
        }

        _health -= damage;

        if (_health <= 0)
        {
            OnDeath?.Invoke();
            _isAlive = false;
        }

        OnHealthChanged?.Invoke();
    }
}
