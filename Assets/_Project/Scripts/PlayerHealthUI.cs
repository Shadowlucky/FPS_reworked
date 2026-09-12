using TMPro;
using UnityEngine;

/// <summary>
/// Отображает здоровье игрока в интерфейсе.
/// </summary>
public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private RectTransform healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    private float maxBarWidth;
    private float barHeight;
    private float barStartPosition;

    private void Awake()
    {
        maxBarWidth = healthBar.sizeDelta.x;
        barHeight = healthBar.sizeDelta.y;
        barStartPosition = healthBar.localPosition.x;
    }

    private void OnEnable()
    {
        health.HealthChanged += UpdateHealthUI;
    }

    private void OnDisable()
    {
        health.HealthChanged -= UpdateHealthUI;
    }

    private void Start()
    {
        UpdateHealthUI(health.CurrentHealth, health.MaxHealth);
    }

    private void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        float healthPercent = currentHealth / maxHealth;
        float currentWidth = maxBarWidth * healthPercent;

        healthBar.sizeDelta = new Vector2(currentWidth, barHeight);
        healthBar.localPosition = new Vector3(
            barStartPosition - (maxBarWidth - currentWidth) / 2f,
            healthBar.localPosition.y,
            healthBar.localPosition.z
        );

        healthText.text = Mathf.CeilToInt(currentHealth).ToString();
    }
}