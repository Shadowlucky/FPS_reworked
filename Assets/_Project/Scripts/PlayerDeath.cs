using UnityEngine;

/// <summary>
/// Обрабатывает смерть игрока.
/// </summary>
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject loseMenu;

    private void OnEnable()
    {
        health.Died += Die;
    }

    private void OnDisable()
    {
        health.Died -= Die;
    }

    private void Die()
    {
        Time.timeScale = 0f;
        loseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}