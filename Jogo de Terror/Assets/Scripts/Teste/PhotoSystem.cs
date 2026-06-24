using UnityEngine;
using System.Collections;

public class PhotoSystem : MonoBehaviour
{
    public EnemyController enemy;

    public Transform player;
    public Transform enemyTransform;

    public float distanciaJumpscare = 5f;

    public int fotosMaximas = 5;
    private int fotosRestantes;

    void Start()
    {
        fotosRestantes = fotosMaximas;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && fotosRestantes > 0)
        {
            StartCoroutine(TirarFoto());
        }
    }

    IEnumerator TirarFoto()
    {
        fotosRestantes--;

        Debug.Log("Fotos restantes: " + fotosRestantes);

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

        if (fotosRestantes <= 0)
        {
            Debug.Log("Você ficou sem fotos!");
        }
    }
}