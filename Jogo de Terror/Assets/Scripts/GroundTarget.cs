using UnityEngine;
using UnityEngine.AI;

public class GroundTarget : MonoBehaviour
{
    [Header("Referências")]
    public Transform submarine;

    [Header("Config")]
    public float searchDistance = 100f;

    private void Update()
    {
        if (submarine == null)
            return;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(
            submarine.position,
            out hit,
            searchDistance,
            NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
    }
}