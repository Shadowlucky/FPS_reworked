using UnityEngine;

/// <summary>
/// Перемещает группу объектов вслед за указанным объектом.
/// </summary>
public class Follow : MonoBehaviour
{
    public GameObject followObj;

    private void Update()
    {
        if (transform.childCount == 0 || followObj == null)
            return;

        transform.position = followObj.transform.position;
    }
}