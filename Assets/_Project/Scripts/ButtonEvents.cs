using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Обрабатывает действия кнопок интерфейса.
/// </summary>
public class ButtonEvents : MonoBehaviour
{
    /// <summary>
    /// Загружает указанную сцену.
    /// </summary>
    public void ChangeScene(string sceneName)
    {
        GameManager.gameScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }
}