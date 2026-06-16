using UnityEngine;

public sealed class Door : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 3f;

    private bool playerNear;
    private bool opened;

    private Inventory inventory;

    private Quaternion closedRotation;
    private Quaternion openedRotation;

    private void Awake()
    {
        closedRotation = transform.rotation;

        openedRotation = Quaternion.Euler(
            transform.eulerAngles + new Vector3(0, openAngle, 0)
        );
    }

    private void Update()
    {
        if (playerNear && inventory.HasKey && Input.GetKeyDown(KeyCode.E))
        {
            opened = true;
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerNear = false;
        inventory = null;
    }
}