using UnityEngine;

/// <summary>
/// Управляет автоматическим открытием и закрытием двери.
/// </summary>
[RequireComponent(typeof(Animator))]
public class DoorScript : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool isOpen;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Переключает состояние двери.
    /// </summary>
    public void Open()
    {
        isOpen = !isOpen;
        animator.SetBool("IsOpen", isOpen);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Open();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            Open();
    }
}