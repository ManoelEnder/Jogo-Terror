using UnityEngine;

public sealed class Inventory : MonoBehaviour
{
    public bool HasKey { get; private set; }
    public bool HasFireExtinguisher { get; private set; }
    public bool HasCamera { get; private set; }

    public void AddKey()
    {
        HasKey = true;
    }

    public void AddFireExtinguisher()
    {
        HasFireExtinguisher = true;
    }

    public void AddCamera()
    {
        HasCamera = true;
    }
}