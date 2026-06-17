using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public sealed class Interactor : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float distance = 3f;
    [SerializeField] private TMP_Text interactionText;

    private void Update()
    {
        interactionText.gameObject.SetActive(false);

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, distance))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactionText.gameObject.SetActive(true);
                interactionText.text = interactable.GetInteractionText();

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    interactable.Interact();
                }
            }
        }
    }
}