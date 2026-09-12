using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Управляет действиями главного меню.
/// </summary>
public class UIMainMenu : MonoBehaviour
{
    /// <summary>
    /// Загружает игровую сцену.
    /// </summary>
    public void PlayLevel()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Завершает работу приложения.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}