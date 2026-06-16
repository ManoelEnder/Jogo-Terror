using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController characterController;
    private InputSystem_Actions inputActions;

    private Vector2 moveInput;
    private Vector3 velocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputActions = new InputSystem_Actions();

        inputActions.Player.Move.performed += context =>
            moveInput = context.ReadValue<Vector2>();

        inputActions.Player.Move.canceled += _ =>
            moveInput = Vector2.zero;

        inputActions.Player.Jump.performed += _ =>
            Jump();
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
        HandleMovement();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        Vector3 direction =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        characterController.Move(
            direction * moveSpeed * Time.deltaTime
        );
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(
            velocity * Time.deltaTime
        );
    }
}