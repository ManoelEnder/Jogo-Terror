using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FireExtintor : MonoBehaviour, IInteractable
{
    private Inventory inventory;
    private Outline outline;

    [Header("Objeto na mão")]
    public GameObject extinguisherInHand;

    private void Awake()
    {
        inventory = FindFirstObjectByType<Inventory>();

        outline = GetComponent<Outline>();

        if (outline != null)
            outline.enabled = false;

        if (extinguisherInHand != null)
            extinguisherInHand.SetActive(false);
    }

    public void Interact()
    {
        Debug.Log("EXTINTOR COLETADO");

        inventory.AddFireExtinguisher();

        if (extinguisherInHand != null)
            extinguisherInHand.SetActive(true);

        Destroy(gameObject);
    }

    public string GetInteractionText()
    {
        return "Clique para pegar o extintor";
    }
}