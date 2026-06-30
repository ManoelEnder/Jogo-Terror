using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Referências")]
    public Camera playerCamera;

    [Header("Config")]
    public float interactDistance = 3f;

    private Outline currentOutline;
    private IInteractable currentInteractable;

    private void Update()
    {
        DetectObject();

        if (currentInteractable != null && Input.GetMouseButtonDown(0))
        {
            Debug.Log("CLICOU NO OBJETO");

            currentInteractable.Interact();

            if (currentOutline != null)
            {
                currentOutline.enabled = false;
                currentOutline = null;
            }

            currentInteractable = null;
        }
    }

    private void DetectObject()
    {
        // Desliga o outline anterior
        if (currentOutline != null)
        {
            currentOutline.enabled = false;
            currentOutline = null;
        }

        currentInteractable = null;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        Debug.DrawRay(
            playerCamera.transform.position,
            playerCamera.transform.forward * interactDistance,
            Color.red
        );

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            Debug.Log("Acertou: " + hit.collider.name);

            currentInteractable =
                hit.collider.GetComponentInParent<IInteractable>();
            Debug.Log("Interactable: " + currentInteractable);

            if (currentInteractable != null)
            {
                currentOutline =
                    hit.collider.GetComponentInParent<Outline>();

                if (currentOutline != null)
                {
                    currentOutline.enabled = true;
                }
            }
        }
    }
}