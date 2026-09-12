using UnityEngine;

/// <summary>
/// Управляет завершением уровня при взаимодействии игрока.
/// </summary>
public class EndTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject winScreen;

    public bool CanInteract => GameManager.player != null && GameManager.player.HasKeyCard;

    public void Interact()
    {
        if (!CanInteract)
            return;

        winScreen.SetActive(true);
        GameManager.openedScreen = true;
        GameManager.player.ShowCursor();
    }
}