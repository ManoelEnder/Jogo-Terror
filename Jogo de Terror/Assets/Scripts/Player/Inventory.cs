using UnityEngine;

public sealed class Inventory : MonoBehaviour
{
    [SerializeField]public bool HasKey { get; private set; }
    [SerializeField]
    
    public bool HasFireExtinguisher { get; private set; }

    public void AddKey()
    {
        HasKey = true;
    }

    public void AddFireExtinguisher()
    {
        HasFireExtinguisher = true;
    }
}