using UnityEngine;

public class KeyItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Inventory inventory))
        {
            inventory.AddKey();
            Destroy(gameObject);
        }
    }
}