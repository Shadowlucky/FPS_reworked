/// <summary>
/// Контракт для объектов, которые могут получать урон.
/// </summary>
public interface IDamageable
{
    void TakeDamage(float damage);
}