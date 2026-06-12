using UnityEngine;
using System.Collections.Generic;

public class RadarTarget : MonoBehaviour
{
     [HideInInspector]public bool detected;
    public static List<RadarTarget> AllTargets =
        new List<RadarTarget>();

    void OnEnable()
    {
        AllTargets.Add(this);
    }

    void OnDisable()
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
