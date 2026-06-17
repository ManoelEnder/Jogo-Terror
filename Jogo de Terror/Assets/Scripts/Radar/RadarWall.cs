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

    [Header("Animação")]
    public float moveSpeed = 3f;
    public float scaleSpeed = 5f;

    private Transform wallBlip;
    private Renderer wallRenderer;
    private bool wasVisible;

    private void Start()
{
    GameObject obj = Instantiate(
        blipPrefab,
        radar
    );

    wallBlip = obj.transform;
    wallRenderer = wallBlip.GetComponentInChildren<Renderer>();
    Debug.Log(wallRenderer);


    if (wallRenderer == null)
        wallRenderer = wallBlip.GetComponentInChildren<Renderer>();

    if (wallRenderer == null)
    {
        Debug.LogError("Renderer não encontrado no prefab!");
        return;
    }

    wallBlip.localPosition = Vector3.zero;
    wallBlip.localScale = Vector3.zero;

    wallRenderer.enabled = false;
}

    private void Update()
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
            if (wallRenderer != null)
                wallRenderer.enabled = false;

            wasVisible = false;
            return;
        }

        if (!wasVisible)
        {
            wasVisible = true;

            wallBlip.localPosition = Vector3.zero;
            wallBlip.localScale = Vector3.zero;

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

        Vector3 targetPos = new Vector3(
            radarPos.x,
            0.05f,
            radarPos.y
        );

        wallBlip.localPosition = Vector3.Lerp(
            wallBlip.localPosition,
            targetPos,
            Time.deltaTime * moveSpeed
        );

        Vector3 desiredScale =
            Vector3.one * 0.08f;

        wallBlip.localScale = Vector3.Lerp(
            wallBlip.localScale,
            desiredScale,
            Time.deltaTime * scaleSpeed
        );
    }

    private void OnDestroy()
    {
        if (wallBlip != null)
            Destroy(wallBlip.gameObject);
    }
}