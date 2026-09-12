using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Управляет действиями кнопок игрового интерфейса.
/// </summary>
public class UIManager : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Перезапускает текущую сцену.
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Возвращает игрока в главное меню.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Завершает работу приложения.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}