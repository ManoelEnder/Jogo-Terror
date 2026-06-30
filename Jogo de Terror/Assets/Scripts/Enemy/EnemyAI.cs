using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Referências")]
    public Transform target;

    [Header("Configuração")]
    public float stopDistance = 2f;

    [HideInInspector]
    public bool IsChasing;

    private NavMeshAgent agent;
    private bool activated;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stopDistance;

        activated = false;
        IsChasing = false;
    }

    public void ActivateEnemy()
    {
        activated = true;
        IsChasing = true;

        Debug.Log("INIMIGO ATIVADO!");
    }

    private void Update()
    {
        if (!activated)
            return;

        if (target == null)
            return;

        agent.SetDestination(target.position);
    }
}