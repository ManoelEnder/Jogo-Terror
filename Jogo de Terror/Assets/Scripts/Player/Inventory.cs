using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool HasKey { get; private set; }

    public void AddKey()
    {
        HasKey = true;
    }
}