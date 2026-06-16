using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 3f;

    private bool playerNear;
    private bool isOpen;

    private Quaternion closedRotation;
    private Quaternion openedRotation;
    private Inventory playerInventory;

    private void Awake()
    {
        closedRotation = transform.rotation;
        openedRotation = Quaternion.Euler(
            transform.eulerAngles + new Vector3(0, openAngle, 0)
        );
    }

    private void Update()
    {
        if (playerNear && playerInventory.HasKey && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = true;
        }

        if (isOpen)
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
        if (other.TryGetComponent(out Inventory inventory))
        {
            playerNear = true;
            playerInventory = inventory;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Inventory inventory))
        {
            playerNear = false;
            playerInventory = null;
        }
    }
}