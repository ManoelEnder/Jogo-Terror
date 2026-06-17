using UnityEngine;

public sealed class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 3f;

    private bool opened;
    private Quaternion openedRotation;
    private Inventory inventory;

    private void Awake()
    {
        inventory = FindFirstObjectByType<Inventory>();

        openedRotation = Quaternion.Euler(
            transform.eulerAngles + new Vector3(0, openAngle, 0)
        );
    }

    public void Interact()
    {
        if (inventory.HasKey)
        {
            opened = true;
        }
    }

    public string GetInteractionText()
    {
        if (inventory.HasKey)
            return "E para abrir";

        return "Precisa de uma chave";
    }

    private void Update()
    {
        if (opened)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                openedRotation,
                Time.deltaTime * openSpeed
            );
        }
    }
}