using UnityEngine;

/// <summary>
/// Хранит настройки и текущее состояние оружия.
/// </summary>
public class Weapon
{
    public float Damage { get; }

    public float FireInterval { get; }

    public int MaxAmmo { get; }

    public int CurrentAmmo { get; private set; }

    public Vector3 Position { get; }

    public Weapon(
        float damage,
        float fireInterval,
        int maxAmmo,
        int currentAmmo,
        Vector3 position)
    {
        Damage = damage;
        FireInterval = fireInterval;
        MaxAmmo = maxAmmo;
        CurrentAmmo = currentAmmo;
        Position = position;
    }

    public void SetCurrentAmmo(int currentAmmo)
    {
        CurrentAmmo = currentAmmo;
    }
}