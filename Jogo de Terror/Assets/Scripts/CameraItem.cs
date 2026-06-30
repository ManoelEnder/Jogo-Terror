using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CameraItem : MonoBehaviour, IInteractable
{
    public GameObject cameraInHand;

    private Inventory inventory;
    private Outline outline;

    private void Awake()
    {
        inventory = FindFirstObjectByType<Inventory>();

        outline = GetComponent<Outline>();

        if (outline != null)
            outline.enabled = false;

        cameraInHand.SetActive(false);
    }

    public void Interact()
    {
        inventory.AddCamera();

        cameraInHand.SetActive(true);

        Destroy(gameObject);
    }

    public string GetInteractionText()
    {
        return "Clique para pegar a câmera";
    }
}