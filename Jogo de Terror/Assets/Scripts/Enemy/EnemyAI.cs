using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float stopDistance = 2f;

    private NavMeshAgent agent;
    private bool activated = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stopDistance;
    }

    public void ActivateEnemy()
    {
        activated = true;
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