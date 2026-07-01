using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Referências")]
    public Transform target;

    [Header("Movimento")]
    public float moveSpeed = 8f;
    public float rotationSpeed = 5f;
    public float stopDistance = 2f;

    [HideInInspector]
    public bool IsChasing;

    private bool activated;

    private void Awake()
    {
        activated = false;
        IsChasing = false;
    }

    public void ActivateEnemy()
    {
        activated = true;
        IsChasing = true;

    }

    private void Update()
    {
        if (!activated)
            return;

        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            transform.position +=
                direction.normalized *
                moveSpeed *
                Time.deltaTime;
        }

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
        }
    }
}