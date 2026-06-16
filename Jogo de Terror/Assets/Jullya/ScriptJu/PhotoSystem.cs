using UnityEngine;
using System.Collections;

public class PhotoSystem : MonoBehaviour
{
    public EnemyController enemy;

    public Transform player;
    public Transform enemyTransform;

    public float distanciaJumpscare = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(TirarFoto());
        }
    }

    IEnumerator TirarFoto()
    {
        enemy.MostrarNaFoto();

        float distancia = Vector3.Distance(
            player.position,
            enemyTransform.position
        );

        if (distancia <= distanciaJumpscare)
        {
            GameManager gm = FindFirstObjectByType<GameManager>();

            if (gm != null)
            {
                gm.Jumpscare();
            }
        }

        yield return new WaitForSeconds(0.3f);

        enemy.Esconder();
    }
}