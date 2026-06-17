using UnityEngine;

public class Radar : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public Transform target;
    public Transform blip;

    [Header("Radar")]
    public float detectionRange = 50f;
    public float radarRadius = 0.8f;

    [Header("Visual")]
    public float minSize = 0.05f;
    public float maxSize = 0.15f;
    public float pulseSpeed = 2f;

    private Renderer blipRenderer;

    private void Start()
    {
        Debug.Log("RADAR NOVO CARREGADO");
        if (player == null)
        {
            Debug.LogError("Player não atribuído!");
            return;
        }

        if (target == null)
        {
            Debug.LogError("Target não atribuído!");
            return;
        }

        if (blip == null)
        {
            Debug.LogError("Blip não atribuído!");
            return;
        }

        blipRenderer = blip.GetComponent<Renderer>();

        if (blipRenderer == null)
            blipRenderer = blip.GetComponentInChildren<Renderer>();

        if (blipRenderer == null)
        {
            Debug.LogError("Nenhum Renderer encontrado no Blip!");
            return;
        }

        blipRenderer.enabled = true;

        blip.localPosition = new Vector3(0f, 0.05f, 0f);
    }

    private void Update()
{
    if (player == null || target == null || blip == null)
        return;

    Vector3 offset = target.position - player.position;
    offset.y = 0f;

    float distance = offset.magnitude;

    if (distance > detectionRange)
    {
        blipRenderer.enabled = false;
        return;
    }

    blipRenderer.enabled = true;

    Vector2 radarPos =
        new Vector2(offset.x, offset.z);

    radarPos = Vector2.ClampMagnitude(
        radarPos,
        radarRadius
    );

    blip.localPosition = new Vector3(
        radarPos.x,
        0.05f,
        radarPos.y
    );

    Debug.Log("RadarPos: " + radarPos);
}
}