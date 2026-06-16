using System.Collections.Generic;
using UnityEngine;

public class RadarTarget : MonoBehaviour
{
    public bool detected;

    public static List<RadarTarget> AllTargets =
        new List<RadarTarget>();

    private void OnEnable()
    {
        AllTargets.Add(this);
    }

    private void OnDisable()
    {
        AllTargets.Remove(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detected = false;
        }
    }
}