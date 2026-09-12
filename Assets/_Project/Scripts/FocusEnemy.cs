using UnityEngine;

/// <summary>
/// Поворачивает оружие в сторону игрока.
/// </summary>
public class FocusEnemy : MonoBehaviour
{
    [Header("Фокус по осям")]
    [SerializeField] private bool all;
    [SerializeField] private bool xz;

    [Header("Дистанция обнаружения")]
    [SerializeField, Range(1f, 300f)] private float focusDistance = 150f;

    private void Start()
    {
        InvokeRepeating(nameof(Look), 1f, 0.01f);
    }

    private void Look()
    {
        if (Vector3.Distance(GameManager.playerPosition, transform.position) > focusDistance)
            return;

        if (xz)
        {
            transform.LookAt(
                new Vector3(
                    GameManager.playerPosition.x,
                    transform.position.y,
                    GameManager.playerPosition.z
                )
            );
        }
        else if (all)
        {
            transform.LookAt(GameManager.playerPosition);
        }
    }
}