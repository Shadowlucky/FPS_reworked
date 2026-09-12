using TMPro;
using UnityEngine;

/// <summary>
/// Управляет стрельбой и перезарядкой текущего оружия.
/// </summary>
public class Fire : MonoBehaviour
{
    [Header("Префаб патрона")]
    [SerializeField] private GameObject ammoPrefab;

    [Header("Камера игрока")]
    [SerializeField] private Camera playerCamera;

    [Header("Точка выстрела")]
    [SerializeField] private Transform ammoPoint;

    [Header("Эффект выстрела")]
    [SerializeField] private ParticleSystem muzzleEffect;

    [Header("Текст боеприпасов")]
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Индикатор перезарядки")]
    [SerializeField] private GameObject reloadCircle;

    [Header("Настройки")]
    [SerializeField] private float bulletSpeed = 50f;
    [SerializeField] private float reloadTime = 3f;

    public bool CanShoot { get; private set; } = true;

    private Weapon CurrentWeapon =>
        GameManager.weapons[GameManager.chosenWeaponID];

    private void Update()
    {
        if (GameManager.openedScreen)
            return;

        HandleShooting();
        HandleReload();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    /// <summary>
    /// Обрабатывает стрельбу игрока.
    /// </summary>
    private void HandleShooting()
    {
        if (!Input.GetMouseButton(0))
            return;

        if (!CanShoot || GameManager.currentAmmo <= 0)
            return;

        Shoot();
    }

    /// <summary>
    /// Обрабатывает запуск перезарядки.
    /// </summary>
    private void HandleReload()
    {
        if (GameManager.isReloading)
            return;

        bool reloadPressed = Input.GetKeyDown(KeyCode.R);
        bool magazineEmpty =
            Input.GetMouseButton(0) && GameManager.currentAmmo == 0;

        if (!reloadPressed && !magazineEmpty)
            return;

        if (GameManager.currentAmmo < GameManager.maxAmmo)
            StartReload();
    }

    /// <summary>
    /// Выполняет выстрел и запускает задержку до следующего выстрела.
    /// </summary>
    private void Shoot()
    {
        CanShoot = false;
        GameManager.currentAmmo--;

        UpdateAmmoText();
        CreateAmmo();

        Invoke(
            nameof(EnableShooting),
            GameManager.fireInterval
        );
    }

    /// <summary>
    /// Создаёт пулю и задаёт ей направление и скорость.
    /// </summary>
    private void CreateAmmo()
    {
        if (ammoPrefab == null || ammoPoint == null || playerCamera == null)
            return;

        Vector3 direction = GetShootDirection();

        GameObject ammo = Instantiate(
            ammoPrefab,
            ammoPoint.position,
            Quaternion.identity
        );

        AmmoDamage ammoDamage = ammo.GetComponent<AmmoDamage>();

        if (ammoDamage != null)
            ammoDamage.SetDamage(CurrentWeapon.Damage);

        Rigidbody rigidbody = ammo.GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.AddForce(
                direction * bulletSpeed,
                ForceMode.Impulse
            );
        }

        if (muzzleEffect != null)
        {
            Instantiate(
                muzzleEffect,
                ammoPoint.position,
                ammoPoint.rotation
            );
        }
    }

    /// <summary>
    /// Определяет направление выстрела из центра камеры к цели.
    /// </summary>
    private Vector3 GetShootDirection()
    {
        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit))
            return (hit.point - ammoPoint.position).normalized;

        return playerCamera.transform.forward;
    }

    /// <summary>
    /// Запускает перезарядку оружия.
    /// </summary>
    private void StartReload()
    {
        GameManager.isReloading = true;
        CanShoot = false;

        if (reloadCircle != null)
            reloadCircle.SetActive(true);

        Invoke(nameof(FinishReload), reloadTime);
    }

    /// <summary>
    /// Завершает перезарядку и восстанавливает боезапас.
    /// </summary>
    private void FinishReload()
    {
        GameManager.currentAmmo = GameManager.maxAmmo;
        GameManager.isReloading = false;
        CanShoot = true;

        UpdateAmmoText();

        if (reloadCircle != null)
            reloadCircle.SetActive(false);
    }

    /// <summary>
    /// Разрешает следующий выстрел после завершения интервала стрельбы.
    /// </summary>
    private void EnableShooting()
    {
        if (!GameManager.isReloading)
            CanShoot = true;
    }

    /// <summary>
    /// Обновляет отображение текущего количества боеприпасов.
    /// </summary>
    private void UpdateAmmoText()
    {
        if (ammoText != null)
            ammoText.text = GameManager.currentAmmo.ToString();
    }
}