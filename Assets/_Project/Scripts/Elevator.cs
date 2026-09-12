using UnityEngine;

/// <summary>
/// Управляет взаимодействием с лифтом.
/// </summary>
[RequireComponent(typeof(Animator))]
public class Elevator : MonoBehaviour, IInteractable
{
    [Header("Настройки")]
    [SerializeField] private float returnDelay = 5f;

    private Animator animator;
    private bool isUp;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Поднимает лифт и запускает его автоматический возврат.
    /// </summary>
    public void Interact()
    {
        if (isUp)
            return;

        isUp = true;
        animator.SetBool("Up", true);

        Invoke(nameof(ReturnDown), returnDelay);
    }

    /// <summary>
    /// Возвращает лифт в исходное положение.
    /// </summary>
    private void ReturnDown()
    {
        animator.SetBool("Up", false);
        isUp = false;
    }
}