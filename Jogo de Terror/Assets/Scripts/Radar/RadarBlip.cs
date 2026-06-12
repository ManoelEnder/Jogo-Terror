using UnityEngine;

public class RadarBlip : MonoBehaviour
{
    public Transform player;
    public Transform target;

    [Header("Distance")]
    public float maxDistance = 50f;

    [Header("Size")]
    public float minSize = 0.1f;
    public float maxSize = 1f;

    [Header("Pulse")]
    public float pulseSpeed = 2f;

    private MeshRenderer meshRenderer;
    private RadarTarget radarTarget;

    private void Start()
    {
        radarTarget = target.GetComponent<RadarTarget>();
        meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.enabled = false;
    }

    private void Update()
    {
        if (!radarTarget.detected)
        {
            meshRenderer.enabled = false;

            transform.localScale = new Vector3(
                minSize,
                transform.localScale.y,
                minSize
            );

            return;
        }

        meshRenderer.enabled = true;

        float distance = Vector3.Distance(
            player.position,
            target.position
        );

        float t = 1f - Mathf.Clamp01(
            distance / maxDistance
        );

        float baseSize = Mathf.Lerp(
            minSize,
            maxSize,
            t
        );

        float pulse = Mathf.PingPong(
            Time.time * pulseSpeed,
            1f
        );

        float finalSize = Mathf.Lerp(
            baseSize * 0.8f,
            baseSize * 1.2f,
            pulse
        );

        transform.localScale = new Vector3(
            finalSize,
            transform.localScale.y,
            finalSize
        );
    }
}