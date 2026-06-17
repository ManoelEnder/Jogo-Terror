using UnityEngine;

public class Radar : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public Transform target;
    public Transform blip;

    [Header("Radar")]
    public float detectionRange = 50f;
    public float radarRadius = 1f;

    [Header("Visual")]
    public float minSize = 0.05f;
    public float maxSize = 0.15f;
    public float pulseSpeed = 2f;

    [Header("Animação")]
    public float moveSpeed = 3f;
    public float scaleSpeed = 5f;

    private Renderer blipRenderer;
    private bool wasVisible;

    private void Start()
    {
        Debug.Log("ENTROU NO ALCANCE");

        blip.localScale = Vector3.zero;
        blipRenderer.enabled = false;
        if (player == null || target == null || blip == null)
        {
            Debug.LogError("Radar não configurado!");
            enabled = false;
            return;
        }

        blipRenderer = blip.GetComponent<Renderer>();

        if (blipRenderer == null)
            blipRenderer = blip.GetComponentInChildren<Renderer>();

        if (blipRenderer == null)
        {
            Debug.LogError("Renderer não encontrado no Blip!");
            enabled = false;
            return;
        }

        blip.localPosition = Vector3.zero;
        blip.localScale = Vector3.zero;
        blipRenderer.enabled = false;
    }

    private void Update()
    {
        Vector3 offset =
            Quaternion.Inverse(player.rotation) *
            (target.position - player.position);

        offset.y = 0f;

        float distance = offset.magnitude;

        if (distance > detectionRange)
        {
            blipRenderer.enabled = false;
            wasVisible = false;
            return;
        }

        if (!wasVisible)
        {
            wasVisible = true;

            blip.localPosition = Vector3.zero;
            blip.localScale = Vector3.zero;

            blipRenderer.enabled = true;
        }

        Vector2 radarPos =
            new Vector2(offset.x, offset.z)
            / detectionRange
            * radarRadius;

        radarPos = Vector2.ClampMagnitude(
            radarPos,
            radarRadius
        );

        Vector3 targetPosition = new Vector3(
            radarPos.x,
            0.05f,
            radarPos.y
        );

        // Movimento suave
        blip.localPosition = Vector3.Lerp(
            blip.localPosition,
            targetPosition,
            Time.deltaTime * moveSpeed
        );

        // Escala pela distância
        float t =
            1f - Mathf.Clamp01(
                distance / detectionRange
            );

        float baseSize =
            Mathf.Lerp(
                minSize,
                maxSize,
                t
            );

        float pulse =
            Mathf.PingPong(
                Time.time * pulseSpeed,
                1f
            );

        float finalSize =
            Mathf.Lerp(
                baseSize * 0.8f,
                baseSize * 1.2f,
                pulse
            );

        Vector3 desiredScale =
            Vector3.one * finalSize;

        blip.localScale = Vector3.Lerp(
            blip.localScale,
            desiredScale,
            Time.deltaTime * scaleSpeed
        );
    }
}