using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Управляет игроком, его взаимодействиями, движением и оружием.
/// </summary>
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Fire))]
public class Player : MonoBehaviour
{
    [Header("Камера")]
    [SerializeField] private Camera playerCamera;

    [Header("Интерфейс")]
    [SerializeField] private TextMeshProUGUI medkitsText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private GameObject keyCardIcon;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TextMeshProUGUI helpCheckText;
    [SerializeField] private GameObject textUse;

    [Header("Оружие")]
    [SerializeField] private List<GameObject> weaponPrefabs = new List<GameObject>();
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject automat;

    [Header("Аптечка")]
    [SerializeField] private float medkitHealAmount = 50f;
    [SerializeField] private float medkitHealTime = 3f;
    [SerializeField] private GameObject medkitProgress;

    private Fire fire;
    private Health health;
    private CharacterController controller;

    private int medkits;
    private bool isHealing;
    private bool hasKeyCard;

    private GameObject weaponObject;

    private float verticalVelocity;
    private float rotationX;
    private float rotationY;

    private void Awake()
    {
        fire = GetComponent<Fire>();
        health = GetComponent<Health>();
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        HideCursor();

        GameManager.playerPosition = transform.position;
        GameManager.openedScreen = false;
        GameManager.player = this;
        GameManager.chosenWeaponID = 0;

        medkits = 0;
        medkitsText.text = "0";

        keyCardIcon.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        textUse.SetActive(false);

        pistol.SetActive(true);
        automat.SetActive(false);

        helpCheckText.text = "Найдите ключ-карту";

        InitializeWeapon(GameManager.chosenWeaponID);
    }

    private void Update()
    {
        if (!GameManager.openedScreen)
        {
            GameManager.playerPosition = transform.position;

            HandleLook();
            HandleMovement();
            HandleWeaponSwitching();
        }

        HandleMedkitUse();
        HandlePause();
    }

    /// <summary>
    /// Блокирует курсор и скрывает его.
    /// </summary>
    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Разблокирует курсор и отображает его.
    /// </summary>
    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Обрабатывает вращение игрока и камеры.
    /// </summary>
    private void HandleLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * Settings.VerticalSensitivity;

        rotationX = Mathf.Clamp(
            rotationX,
            Settings.MinVerticalAngle,
            Settings.MaxVerticalAngle
        );

        rotationY += Input.GetAxis("Mouse X") * Settings.HorizontalSensitivity;

        transform.localEulerAngles = new Vector3(
            0f,
            rotationY,
            0f
        );

        playerCamera.transform.localEulerAngles = new Vector3(
            rotationX,
            0f,
            0f
        );
    }

    /// <summary>
    /// Инициализирует выбранное оружие.
    /// </summary>
    private void InitializeWeapon(int weaponID)
    {
        Weapon weapon = GameManager.weapons[weaponID];

        GameManager.fireInterval = weapon.FireInterval;
        GameManager.maxAmmo = weapon.MaxAmmo;
        GameManager.currentAmmo = weapon.CurrentAmmo;

        ammoText.text = GameManager.currentAmmo.ToString();

        if (weaponObject != null)
            Destroy(weaponObject);

        weaponObject = Instantiate(
            weaponPrefabs[weaponID],
            transform.position,
            Quaternion.identity
        );

        Transform weaponHolder = transform.GetChild(0);

        weaponObject.transform.SetParent(weaponHolder);
        weaponObject.transform.localPosition = weapon.Position;
        weaponObject.transform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// Обрабатывает движение, прыжок и гравитацию игрока.
    /// </summary>
    private void HandleMovement()
    {
        float speed = Settings.PlayerSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed *= 1.5f;

        if (Input.GetKey(KeyCode.LeftControl))
            speed /= 2.5f;

        float moveForward = Input.GetAxisRaw("Vertical");
        float moveSide = Input.GetAxisRaw("Horizontal");

        Vector3 direction =
            transform.forward * moveForward +
            transform.right * moveSide;

        if (direction.sqrMagnitude > 1f)
            direction.Normalize();

        Vector3 movement = direction * speed;

        if (controller.isGrounded)
        {
            if (verticalVelocity < 0f)
                verticalVelocity = -2f;

            if (Input.GetKey(KeyCode.Space))
            {
                verticalVelocity = Mathf.Sqrt(
                    Settings.PlayerJumpForce * -2f * Physics.gravity.y
                );
            }
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        movement.y = verticalVelocity;

        controller.Move(movement * Time.deltaTime);
    }

    /// <summary>
    /// Обрабатывает переключение оружия.
    /// </summary>
    private void HandleWeaponSwitching()
    {
        if (GameManager.isReloading)
            return;
        
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.mouseScrollDelta.y < 0)
            SwitchWeapon(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.mouseScrollDelta.y > 0)
            SwitchWeapon(1);
    }

    /// <summary>
    /// Переключает оружие на указанный слот.
    /// </summary>
    private void SwitchWeapon(int weaponID)
    {
        if (GameManager.chosenWeaponID == weaponID)
            return;

        GameManager.weapons[GameManager.chosenWeaponID]
            .SetCurrentAmmo(GameManager.currentAmmo);

        GameManager.chosenWeaponID = weaponID;

        pistol.SetActive(weaponID == 0);
        automat.SetActive(weaponID == 1);

        InitializeWeapon(weaponID);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.gameObject.CompareTag("KeyCard"))
            return;

        Destroy(hit.gameObject);

        hasKeyCard = true;
        keyCardIcon.SetActive(true);
        helpCheckText.text = "Вернитесь в лифт";
    }

    /// <summary>
    /// Обрабатывает использование аптечки.
    /// </summary>
    private void HandleMedkitUse()
    {
        if (GameManager.openedScreen)
            return;

        if (isHealing || medkits <= 0 || !Input.GetKeyDown(KeyCode.H))
            return;

        if (health.IsDead || health.CurrentHealth >= health.MaxHealth)
            return;

        StartCoroutine(HealCoroutine());
    }

    /// <summary>
    /// Обрабатывает паузу игры.
    /// </summary>
    private void HandlePause()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        if (!pauseScreen.activeSelf)
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            GameManager.openedScreen = true;
            ShowCursor();
        }
        else
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            GameManager.openedScreen = false;
            HideCursor();
        }
    }

    /// <summary>
    /// Добавляет аптечки в инвентарь игрока.
    /// </summary>
    public bool TryAddMedkits(int amount)
    {
        if (amount <= 0 || medkits > 0)
            return false;

        medkits += amount;
        medkitsText.text = medkits.ToString();

        return true;
    }

    public bool HasKeyCard => hasKeyCard;

    /// <summary>
    /// Выполняет лечение после завершения времени применения аптечки.
    /// </summary>
    private IEnumerator HealCoroutine()
    {
        isHealing = true;

        if (medkitProgress != null)
            medkitProgress.SetActive(true);

        if (fire != null)
            fire.enabled = false;

        yield return new WaitForSeconds(medkitHealTime);

        health.Heal(medkitHealAmount);

        medkits--;
        medkitsText.text = medkits.ToString();

        if (medkitProgress != null)
            medkitProgress.SetActive(false);

        if (fire != null)
            fire.enabled = true;

        isHealing = false;
    }
}