using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public EnemyAI enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Submarino"))
        {
            enemy.ActivateEnemy();

            Debug.Log("TRIGGER ACIONADO");
        }
    }
}