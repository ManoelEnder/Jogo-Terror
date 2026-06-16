using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private float sensitivity = 0.2f;

    private InputSystem_Actions inputActions;
    private Vector2 lookInput;
    private float verticalRotation;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Look.performed += context =>
            lookInput = context.ReadValue<Vector2>();

        inputActions.Player.Look.canceled += _ =>
            lookInput = Vector2.zero;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        Vector2 mouseDelta = lookInput * sensitivity;

        verticalRotation -= mouseDelta.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.localRotation =
            Quaternion.Euler(verticalRotation, 0f, 0f);

        playerBody.Rotate(
            Vector3.up * mouseDelta.x
        );
    }
}