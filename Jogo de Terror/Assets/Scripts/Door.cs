using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public sealed class Door : MonoBehaviour
{
    [SerializeField] private GameObject openText;
    [SerializeField] private GameObject lockedText;

    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 3f;

    private Inventory inventory;
    private bool playerNear;
    private bool opened;

    private Quaternion openedRotation;

    private void Awake()
    {
        openedRotation = Quaternion.Euler(
            transform.eulerAngles + new Vector3(0, openAngle, 0)
        );

        openText.SetActive(false);
        lockedText.SetActive(false);
    }

    private void Update()
    {
        if (!playerNear || inventory == null)
            return;

        if (Keyboard.current.eKey.wasPressedThisFrame && inventory.HasKey)
        {
            opened = true;
            openText.SetActive(false);
        }

        if (opened)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                openedRotation,
                Time.deltaTime * openSpeed
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Inventory playerInventory))
        {
            inventory = playerInventory;
            playerNear = true;

            if (inventory.HasKey)
            {
                openText.SetActive(true);
            }
            else
            {
                lockedText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Inventory playerInventory))
        {
            playerNear = false;
            inventory = null;

            openText.SetActive(false);
            lockedText.SetActive(false);
        }
    }
}