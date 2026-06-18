using UnityEngine;

public class RadarWall : MonoBehaviour
{
    [Header("Radar")]
    public Transform player;
    public Transform radar;

    [Header("Blip")]
    public GameObject blipPrefab;

    [Header("Config")]
    public float detectionRange = 50f;
    public float radarRadius = 1f;

    [Header("Visual")]
    public float blipSize = 2f;

    [Header("Animação")]
    public float moveSpeed = 5f;
    public float scaleSpeed = 8f;

    private Transform wallBlip;
    private Renderer wallRenderer;
    private bool wasVisible;

    void Start()
    {
        Debug.Log("INICIANDO RADAR WALL -> " + gameObject.name);

        if (player == null)
        {
            Debug.LogError(gameObject.name + " PLAYER NÃO DEFINIDO");
            return;
        }

        if (radar == null)
        {
            Debug.LogError(gameObject.name + " RADAR NÃO DEFINIDO");
            return;
        }

        if (blipPrefab == null)
        {
            Debug.LogError(gameObject.name + " BLIP PREFAB NÃO DEFINIDO");
            return;
        }

        GameObject obj = Instantiate(blipPrefab, radar);

        wallBlip = obj.transform;

        wallRenderer = wallBlip.GetComponent<Renderer>();

        if (wallRenderer == null)
            wallRenderer = wallBlip.GetComponentInChildren<Renderer>(true);

        Debug.Log(
            gameObject.name +
            " Renderer encontrado: " +
            wallRenderer
        );

        wallBlip.localPosition = Vector3.zero;
        wallBlip.localScale = Vector3.zero;

        if (wallRenderer != null)
            wallRenderer.enabled = false;
    }

    void Update()
    {
        if (player == null ||
            radar == null ||
            wallBlip == null)
            return;

        Vector3 offset =
            Quaternion.Inverse(player.rotation) *
            (transform.position - player.position);

        offset.y = 0f;

        float distance = offset.magnitude;

      

        if (distance > detectionRange)
        {
            if (wasVisible)
            {
            }

            wasVisible = false;

            if (wallRenderer != null)
                wallRenderer.enabled = false;

            wallBlip.localScale = Vector3.zero;

            return;
        }

        if (!wasVisible)
        {
            Debug.Log(
                gameObject.name +
                " ENTROU NO ALCANCE"
            );

            wasVisible = true;

            if (wallRenderer != null)
                wallRenderer.enabled = true;
        }

        Vector2 radarPos =
            new Vector2(offset.x, offset.z)
            / detectionRange
            * radarRadius;

        radarPos = Vector2.ClampMagnitude(
            radarPos,
            radarRadius
        );

        Vector3 targetPos =
            new Vector3(
                radarPos.x,
                0.05f,
                radarPos.y
            );

        wallBlip.localPosition = Vector3.Lerp(
            wallBlip.localPosition,
            targetPos,
            Time.deltaTime * moveSpeed
        );

        wallBlip.localScale = Vector3.one * 0.5f;
        Debug.Log("LOCAL SCALE = " + wallBlip.localScale);
        Debug.Log("LOSSY SCALE = " + wallBlip.lossyScale);
    }

    private void OnDestroy()
    {
        if (wallBlip != null)
            Destroy(wallBlip.gameObject);
    }
}