using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    public void MostrarNaFoto()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = true;
        }
    }

    public void Esconder()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gm = FindFirstObjectByType<GameManager>();

            if (gm != null)
            {
                gm.Jumpscare();
            }
        }
    }
}