using System;
using UnityEngine;

/// <summary>
/// Компонент, отвечающий за здоровье и получение урона.
/// </summary>
public class Health : MonoBehaviour, IDamageable
{
    [Header("Здоровье")]
    [SerializeField] private float maxHealth = 100f;

    [Header("Броня")]
    [SerializeField] private float armor = 0f;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => maxHealth;
    public float Armor => armor;
    public bool IsDead => CurrentHealth <= 0f;

    public event Action Died;
    public event Action<float, float> HealthChanged;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    /// <summary>
    /// Наносит объекту урон с учётом брони.
    /// </summary>
    public void TakeDamage(float damage)
    {
        if (damage <= 0f || IsDead)
            return;

        float finalDamage = Mathf.Max(damage - armor, 0f);
        CurrentHealth -= finalDamage;

        HealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (IsDead)
        {
            CurrentHealth = 0f;
            HealthChanged?.Invoke(CurrentHealth, MaxHealth);
            Died?.Invoke();
        }
    }

    /// <summary>
    /// Восстанавливает здоровье объекта.
    /// </summary>
    public void Heal(float amount)
    {
        if (amount <= 0f || IsDead)
            return;

        float previousHealth = CurrentHealth;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);

        if (!Mathf.Approximately(previousHealth, CurrentHealth))
            HealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
}