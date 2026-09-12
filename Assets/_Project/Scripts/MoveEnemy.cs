using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Перемещает объект между заданными точками по NavMesh.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class MoveEnemy : MonoBehaviour
{
    [Header("Точки перемещения")]
    [SerializeField] private List<GameObject> movementPoints = new();

    private NavMeshAgent agent;
    private int currentPointIndex;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetNextDestination();
    }

    private void Update()
    {
        if (movementPoints.Count == 0 || agent.pathPending)
            return;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPointIndex = (currentPointIndex + 1) % movementPoints.Count;
            SetNextDestination();
        }
    }

    private void SetNextDestination()
    {
        if (movementPoints.Count == 0)
            return;

        agent.SetDestination(movementPoints[currentPointIndex].transform.position);
    }
}