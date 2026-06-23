using UnityEngine;

public class RadarWall : MonoBehaviour
{
    [Header("Radar")]
    public Transform player;
    public Transform radar;

    [Header("Blip")]
    public GameObject blipPrefab;

    [Header("Config")]
    public float detectionRange = 10f;
    public float radarRadius = 1f;

    [Header("Visual")]
    public float blipSize = 0.5f;

    [Header("Animação")]
    public float moveSpeed = 5f;

    private Transform wallBlip;
    private Renderer wallRenderer;
    private bool isVisible;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("PLAYER NÃO DEFINIDO EM " + gameObject.name);
            enabled = false;
            return;
        }

        if (radar == null)
        {
            Debug.LogError("RADAR NÃO DEFINIDO EM " + gameObject.name);
            enabled = false;
            return;
        }

        if (blipPrefab == null)
        {
            Debug.LogError("BLIP PREFAB NÃO DEFINIDO EM " + gameObject.name);
            enabled = false;
            return;
        }

        GameObject obj = Instantiate(blipPrefab, radar);

        wallBlip = obj.transform;

        wallRenderer =
            wallBlip.GetComponentInChildren<Renderer>(true);

        if (wallRenderer == null)
        {
            Debug.LogError("Renderer não encontrado!");
            enabled = false;
            return;
        }

        wallBlip.localPosition = Vector3.zero;
        wallBlip.localScale = Vector3.one * blipSize;

        wallRenderer.enabled = false;

        Debug.Log("RadarWall iniciado: " + gameObject.name);
    }

    private void Update()
    {
        if (wallBlip == null || player == null)
            return;

        float distance = Vector3.Distance(
     player.position,
     transform.position
 );

        if (gameObject.name == "Parede1")
        {
            Debug.Log(
                "Parede1 Distância = " +
                distance
            );
        }

        Debug.Log(
            gameObject.name +
            " | Distância = " +
            distance +
            " | Range = " +
            detectionRange
        );

        if (distance > detectionRange)
        {
            if (isVisible)
            {
                Debug.Log(
                    gameObject.name +
                    " FORA DO ALCANCE"
                );

                isVisible = false;
                wallRenderer.enabled = false;
            }

            return;
        }

        if (!isVisible)
        {
            Debug.Log(
                gameObject.name +
                " ENTROU NO ALCANCE"
            );

            isVisible = true;
            wallRenderer.enabled = true;
        }

        Vector3 offset =
            Quaternion.Inverse(player.rotation) *
            (transform.position - player.position);

        offset.y = 0f;

        Vector2 radarPos =
            new Vector2(offset.x, offset.z)
            / detectionRange
            * radarRadius;

        radarPos =
            Vector2.ClampMagnitude(
                radarPos,
                radarRadius
            );

        Vector3 targetPos =
            new Vector3(
                radarPos.x,
                0.05f,
                radarPos.y
            );

        wallBlip.localPosition =
            Vector3.Lerp(
                wallBlip.localPosition,
                targetPos,
                Time.deltaTime * moveSpeed
            );

        wallBlip.localScale =
            Vector3.one * blipSize;
    }

    private void OnDestroy()
    {
        if (wallBlip != null)
            Destroy(wallBlip.gameObject);
    }
}