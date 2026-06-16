using UnityEngine;

public sealed class Inventory : MonoBehaviour
{
    public bool HasKey { get; private set; }

    public void AddKey()
    {
        HasKey = true;
    }
}