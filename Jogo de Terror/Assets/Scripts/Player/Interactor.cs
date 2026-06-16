using UnityEngine;
using UnityEngine.InputSystem;

public sealed class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private Camera playerCamera;

    private void Update()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.ShowText();

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    interactable.Interact();
                }
            }
        }
    }
}