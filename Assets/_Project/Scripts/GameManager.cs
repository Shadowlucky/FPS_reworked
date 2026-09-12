using UnityEngine;

/// <summary>
/// Хранит глобальное состояние игры и параметры текущего оружия.
/// </summary>
public static class GameManager
{
    public static bool isReloading;
    public static bool openedScreen;

    public static int currentAmmo;
    public static int maxAmmo;
    public static int chosenWeaponID;

    public static float fireInterval = 0.5f;

    public static Vector3 playerPosition;

    public static string gameScene = "TestScene";

    public static Player player;

    public static Weapon[] weapons =
    {
        new Weapon(
            2.5f,
            0.3f,
            10,
            10,
            new Vector3(0.5f, -0.5f, 1.5f)
        ),

        new Weapon(
            1f,
            0.1f,
            40,
            40,
            new Vector3(0.4f, -0.4f, 0.5f)
        )
    };
}