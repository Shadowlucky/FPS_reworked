using UnityEngine;

/// <summary>
/// Управляет аптечкой и её анимацией.
/// </summary>
[RequireComponent(typeof(Animator))]
public class Medkit : MonoBehaviour, IInteractable
{
    [SerializeField] private int medkitAmount = 3;
    [SerializeField] private GameObject supplies;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            animator.SetBool("IsOpen", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            animator.SetBool("IsOpen", false);
    }

    /// <summary>
    /// Добавляет аптечки игроку и убирает содержимое контейнера.
    /// </summary>
    public void Interact()
    {
        Player player = GameManager.player;

        if (player == null)
            return;

        if (!player.TryAddMedkits(medkitAmount))
            return;

        supplies.SetActive(false);
    }
}