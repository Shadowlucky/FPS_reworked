using TMPro;
using UnityEngine;

/// <summary>
/// Управляет взаимодействием игрока с доступными объектами.
/// </summary>
public class Interactor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUse;

    private IInteractable currentInteractable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
            currentInteractable.Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable == null)
            return;

        if (interactable is EndTrigger endTrigger && !endTrigger.CanInteract)
            return;

        currentInteractable = interactable;
        textUse.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != currentInteractable)
            return;

        currentInteractable = null;
        textUse.gameObject.SetActive(false);
    }
}