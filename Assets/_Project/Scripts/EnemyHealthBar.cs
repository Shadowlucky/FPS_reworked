using TMPro;
using UnityEngine;

/// <summary>
/// Отображает здоровье врага и поворачивает индикатор к игроку.
/// </summary>
public class EnemyHealthBar : MonoBehaviour
{
    private RectTransform healthBar;
    private TextMeshProUGUI healthText;
    private Camera playerCamera;
    private Health health;

    private void Awake()
    {
        healthBar = transform.Find("Шкала")?.GetComponent<RectTransform>();
        healthText = GetComponentInChildren<TextMeshProUGUI>();
        playerCamera = Camera.main;
    }

    private void Start()
    {
        if (health == null)
            health = GetComponentInParent<Health>();

        if (health == null)
            return;

        health.HealthChanged += UpdateBar;
        health.Died += Hide;

        UpdateBar(health.CurrentHealth, health.MaxHealth);
    }

    private void OnDisable()
    {
        if (health == null)
            return;

        health.HealthChanged -= UpdateBar;
        health.Died -= Hide;
    }

    private void LateUpdate()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        if (playerCamera == null)
            return;

        transform.LookAt(
            transform.position + playerCamera.transform.rotation * Vector3.forward,
            playerCamera.transform.rotation * Vector3.up
        );

        transform.Rotate(0f, 180f, 0f);
    }

    private void UpdateBar(float currentHealth, float maxHealth)
    {
        if (healthBar == null || healthText == null)
            return;

        float normalizedHealth = currentHealth / maxHealth;

        healthText.text = Mathf.CeilToInt(currentHealth).ToString();

        healthBar.sizeDelta = new Vector2(
            normalizedHealth * 100f,
            20f
        );

        healthBar.localPosition = new Vector3(
            (100f - normalizedHealth * 100f) / 200f,
            0f,
            0f
        );
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}