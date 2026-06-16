using UnityEngine;

public class Radar : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public Transform target;
    public GameObject blipPrefab;

    private Transform blipInstance;
    private Renderer blipRenderer;

    [Header("Radar")]
    public float detectionRange = 50f;
    public float contactRange = 5f;
    public float radarRadius = 4f;

    [Header("Visual")]
    public float minSize = 0.1f;
    public float maxSize = 1f;
    public float pulseSpeed = 2f;

    private bool detected;
    private bool contacted;

    private void Start()
    {
        GameObject obj = Instantiate(blipPrefab, transform);
        blipInstance = obj.transform;

        blipRenderer = obj.GetComponentInChildren<Renderer>();

        if (blipRenderer != null)
            blipRenderer.enabled = false;
    }

   private void Update()
{
    if (player == null || target == null || blipInstance == null)
        return;

    Vector3 offset = target.position - player.position;
    offset.y = 0f;  

    float distance = offset.magnitude;

    if (distance > detectionRange)
    {
        if (blipRenderer != null) blipRenderer.enabled = false;
        detected = false;
        contacted = false;
        return;
    }

    if (blipRenderer != null) blipRenderer.enabled = true;

    if (!detected && distance <= detectionRange) detected = true;

    if (!contacted && distance <= contactRange)
    {
        contacted = true;
        OnContact();
    }

    Vector2 dir = new Vector2(offset.x, offset.z);
    if (dir.magnitude > 0.001f)
        dir = dir.normalized;
    else
        dir = Vector2.zero;

    float distanceRatio = distance / detectionRange;
    float safeRadius = radarRadius * 0.85f;

    Vector3 localPos = new Vector3(
        dir.x * distanceRatio * safeRadius,
        0.02f, 
        dir.y * distanceRatio * safeRadius
    );

    blipInstance.localPosition = localPos;

    float t = 1f - Mathf.Clamp01(distance / detectionRange);
    float baseSize = Mathf.Lerp(minSize, maxSize, t);
    float pulse = Mathf.PingPong(Time.time * pulseSpeed, 1f);
    float finalSize = Mathf.Lerp(baseSize * 0.8f, baseSize * 1.2f, pulse);

    Vector3 parentScale = transform.lossyScale;
    blipInstance.localScale = new Vector3(
        finalSize / parentScale.x,
        finalSize / parentScale.y,
        finalSize / parentScale.z
    );
}

    private void OnContact()
    {

        blipInstance.localScale = Vector3.zero;

        if (blipRenderer != null)
            blipRenderer.enabled = false;
    }
}
