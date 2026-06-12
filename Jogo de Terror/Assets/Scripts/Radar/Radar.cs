using UnityEngine;

public class Radar : MonoBehaviour
{
   public Transform player;

    public Transform radarCenter;

    public GameObject echoPrefab;

    public float scanInterval = 2f;

    public float detectionRange = 50f;

    public float radarRadius = 4f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= scanInterval)
        {
            timer = 0f;
            Scan();
        }
    }

    private void Scan()
    {
        foreach (RadarTarget target in RadarTarget.AllTargets)
        {
            if (target == null)
                continue;

            Vector3 offset =target.transform.position -player.position;

            float distance = offset.magnitude;

            if (distance > detectionRange)
                continue;

            offset.y = 0;

            Vector2 radarPosition =
                new Vector2(offset.x, offset.z)
                / detectionRange
                * radarRadius;

            GameObject echo =Instantiate(echoPrefab,radarCenter);

            echo.transform.localPosition =new Vector3(radarPosition.x,0f,radarPosition.y);      
        }
    }
}