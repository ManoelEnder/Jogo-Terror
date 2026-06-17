using UnityEngine;

public sealed class KeyItem : MonoBehaviour, IInteractable
{
    private Inventory inventory;

    private void Awake()
    {
        inventory = FindFirstObjectByType<Inventory>();
    }

    public void Interact()
    {
        inventory.AddKey();
        Destroy(gameObject);
    }

    public string GetInteractionText()
    {
        return "E para coletar";
    }
}