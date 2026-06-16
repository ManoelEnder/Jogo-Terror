using UnityEngine;

public class RadarBlip : MonoBehaviour
{
    public float lifeTime = 1f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}