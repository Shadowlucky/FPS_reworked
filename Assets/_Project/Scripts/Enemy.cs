using UnityEngine;

/// <summary>
/// Базовый компонент врага.
/// </summary>
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [Header("Лут")]
    [SerializeField] private GameObject lootPrefab;

    [Header("Индикатор здоровья")]
    [SerializeField] private Canvas healthBarPrefab;
    [SerializeField] private float healthBarHeight = 1.5f;
    [SerializeField] private float healthBarScale = 1f;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.Died += Die;
    }

    private void OnDisable()
    {
        health.Died -= Die;
    }

    private void Start()
    {
        CreateHealthBar();
    }

    /// <summary>
    /// Создаёт индикатор здоровья над врагом.
    /// </summary>
    private void CreateHealthBar()
    {
        if (healthBarPrefab == null)
            return;

        Canvas healthBar = Instantiate(
            healthBarPrefab,
            transform.position + Vector3.up * healthBarHeight,
            Quaternion.identity
        );

        healthBar.transform.SetParent(transform);
        healthBar.transform.localScale = Vector3.one * healthBarScale;
    }
    
    /// <summary>
    /// Обрабатывает смерть врага и создание лута.
    /// </summary>
    private void Die()
    {
        if (lootPrefab != null)
            Instantiate(lootPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}