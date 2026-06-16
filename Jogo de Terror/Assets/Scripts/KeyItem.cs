using UnityEngine;
using UnityEngine.InputSystem;

public sealed class KeyItem : MonoBehaviour
{
    [SerializeField] private GameObject interactionText;

    private Inventory inventory;
    private bool playerNear;

    private void Update()
    {
        if (playerNear && Keyboard.current.eKey.wasPressedThisFrame)
        {
            inventory.AddKey();
            interactionText.SetActive(false);
            Destroy(gameObject);
            Debug.Log(playerNear);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Inventory playerInventory))
        {
            inventory = playerInventory;
            playerNear = true;

            interactionText.SetActive(true);
            Debug.Log("Entrou na chave");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Inventory playerInventory))
        {
            playerNear = false;
            interactionText.SetActive(false);
        }
    }
}