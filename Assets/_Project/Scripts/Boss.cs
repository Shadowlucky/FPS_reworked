using UnityEngine;

/// <summary>
/// Управляет фазами босса, дронами и защитным щитом.
/// </summary>
[RequireComponent(typeof(Health))]
public class Boss : MonoBehaviour
{
    [Header("Дроны")]
    [SerializeField] private GameObject dronesPrefab;

    [Header("Щит")]
    [SerializeField] private GameObject shieldPrefab;

    private Health health;
    private GameObject drones;
    private GameObject shield;

    private bool secondPhaseStarted;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.HealthChanged += HandleHealthChanged;
    }

    private void OnDisable()
    {
        health.HealthChanged -= HandleHealthChanged;
    }

    private void Update()
    {
        if (secondPhaseStarted &&
            drones != null &&
            drones.transform.childCount == 0)
        {
            DisableShield();
        }
    }

    /// <summary>
    /// Проверяет здоровье босса и запускает вторую фазу.
    /// </summary>
    private void HandleHealthChanged(float currentHealth, float maxHealth)
    {
        if (secondPhaseStarted)
            return;

        if (currentHealth <= maxHealth * 0.5f)
            StartSecondPhase();
    }

    /// <summary>
    /// Запускает вторую фазу босса, создавая дронов и щит.
    /// </summary>
    private void StartSecondPhase()
    {
        if (dronesPrefab == null || shieldPrefab == null)
            return;

        secondPhaseStarted = true;

        drones = Instantiate(
            dronesPrefab,
            transform.position,
            Quaternion.identity
        );

        shield = Instantiate(
            shieldPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );

        if (drones.TryGetComponent<Follow>(out Follow follow))
            follow.followObj = gameObject;
    }

    /// <summary>
    /// Убирает щит после уничтожения всех дронов.
    /// </summary>
    private void DisableShield()
    {
        if (shield == null)
            return;

        Destroy(shield);
        shield = null;
    }
}