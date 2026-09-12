using UnityEngine;

/// <summary>
/// Управляет стрельбой противника в сторону игрока.
/// </summary>
public class EnemyFire : MonoBehaviour
{
    [Header("Префаб патрона")]
    [SerializeField] private GameObject ammoPrefab;

    [Header("Расстояние атаки")]
    [SerializeField, Range(3f, 100f)] private float fireDistance = 20f;

    [Header("Активность бота")]
    [SerializeField] private bool botActivity = true;

    [Header("Дуло")]
    [SerializeField] private Transform gun;

    [Header("Скорость стрельбы")]
    [SerializeField, Range(0.01f, 10f)] private float fireInterval = 5f;

    [Header("Урон")]
    [SerializeField, Range(0f, 20f)] private float damage = 2f;

    private void Start()
    {
        InvokeRepeating(nameof(CreateAmmo), 2f, fireInterval);
    }

    /// <summary>
    /// Создаёт пулю, если противник активен и игрок находится в пределах дистанции атаки.
    /// </summary>
    private void CreateAmmo()
    {
        if (!botActivity || ammoPrefab == null || gun == null)
            return;

        if (Vector3.Distance(GameManager.playerPosition, transform.position) > fireDistance)
            return;

        GameObject ammo = Instantiate(
            ammoPrefab,
            gun.position + gun.forward * 0.65f,
            Quaternion.identity
        );

        AmmoDamage ammoDamage = ammo.GetComponent<AmmoDamage>();

        if (ammoDamage != null)
            ammoDamage.SetDamage(damage);

        Rigidbody rigidbody = ammo.GetComponent<Rigidbody>();

        if (rigidbody != null)
            rigidbody.AddForce(
                gun.forward * 5f,
                ForceMode.Impulse
            );
    }
}