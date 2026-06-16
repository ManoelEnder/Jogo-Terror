using UnityEngine;

public class Radar : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public Transform target;
    public Transform blip;

    [Header("Radar")]
    public float detectionRange = 50f;
    public float radarRadius = 4f;

    [Header("Visual")]
    public float minSize = 0.1f;
    public float maxSize = 1f;
    public float pulseSpeed = 2f;

    private MeshRenderer blipRenderer;

    private void Start()
    {
        blipRenderer = blip.GetComponent<MeshRenderer>();

        if (blipRenderer != null)
            blipRenderer.enabled = false;
    }

    private void Update()
    {
        if (player == null || target == null || blip == null)
            return;

        Vector3 offset = target.position - player.position;
        offset.y = 0f;

        float distance = offset.magnitude;

        // Esconde se estiver fora do alcance
        if (distance > detectionRange)
        {
            blipRenderer.enabled = false;
            return;
        }

        blipRenderer.enabled = true;

        Vector2 radarPos =
            Vector2.ClampMagnitude(
                new Vector2(offset.x, offset.z)
                / detectionRange
                * radarRadius,
                radarRadius
            );

        blip.localPosition = new Vector3(
            radarPos.x,
            0.1f,
            radarPos.y
        );

        float t = 1f - Mathf.Clamp01(distance / detectionRange);

        float baseSize = Mathf.Lerp(
            minSize,
            maxSize,
            t
        );

        // Pulso
        float pulse = Mathf.PingPong(
            Time.time * pulseSpeed,
            1f
        );

        float finalSize = Mathf.Lerp(
            baseSize * 0.8f,
            baseSize * 1.2f,
            pulse
        );

        blip.localScale = new Vector3(
            finalSize,
            0.02f,
            finalSize
        );
    }
}