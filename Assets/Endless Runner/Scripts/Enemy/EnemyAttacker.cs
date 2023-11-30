public class EnemyAttacker
{
    private float _damage;

    public EnemyAttacker(float damage)
    {
        _damage = damage;
    }

    public void Attack(Player player)
    {
        player.TakeDamage(_damage);
    }
}
