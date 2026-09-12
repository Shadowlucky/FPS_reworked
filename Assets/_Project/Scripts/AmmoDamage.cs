using UnityEngine;

/// <summary>
/// Наносит урон объекту при столкновении.
/// </summary>
public class AmmoDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField, Min(0.1f)] private float lifetime = 10f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    /// <summary>
    /// Устанавливает урон патрона.
    /// </summary>
    public void SetDamage(float value)
    {
        damage = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
            damageable.TakeDamage(damage);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
            damageable.TakeDamage(damage);

        Destroy(gameObject);
    }
}