using UnityEngine;

/// <summary>
/// Постоянно вращает объект вокруг выбранных осей.
/// </summary>
public class Rotation : MonoBehaviour
{
    [Header("Скорость вращения")]
    [SerializeField, Range(-100f, 100f)] private float speedX = 3f;
    [SerializeField, Range(-100f, 100f)] private float speedY = 3f;
    [SerializeField, Range(-100f, 100f)] private float speedZ = 3f;

    [Header("Оси вращения")]
    [SerializeField] private bool rotateX;
    [SerializeField] private bool rotateY = true;
    [SerializeField] private bool rotateZ;

    private void FixedUpdate()
    {
        if (rotateX)
            transform.Rotate(speedX, 0f, 0f);

        if (rotateY)
            transform.Rotate(0f, speedY, 0f);

        if (rotateZ)
            transform.Rotate(0f, 0f, speedZ);
    }
}