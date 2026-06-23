using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SubmarinoController : MonoBehaviour
{
    [Header("Movimento")]
    public float acceleration = 3f;
    public float reverseAcceleration = 2f;
    public float maxSpeed = 8f;
    public float drag = 0.98f;

    [Header("Rotação")]
    public float turnSpeed = 50f;

    [Header("Profundidade")]
    public float verticalSpeed = 3f;

    private Rigidbody rb;
    private float currentSpeed;
    private float verticalInput;
    private SubmarineHealth health;

    private void Awake()
    {
        health = GetComponent<SubmarineHealth>();
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;

        rb.constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Frente / Ré
        if (moveInput > 0)
            currentSpeed += acceleration * moveInput * Time.deltaTime;

        if (moveInput < 0)
            currentSpeed += reverseAcceleration * moveInput * Time.deltaTime;

        currentSpeed = Mathf.Clamp(
            currentSpeed,
            -maxSpeed * 0.5f,
            maxSpeed
        );

        // Rotação
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            transform.Rotate(
                Vector3.up,
                turnInput * turnSpeed * Time.deltaTime
            );
        }

        // Subir / Descer
        verticalInput = 0f;

        if (Input.GetKey(KeyCode.Space))
            verticalInput = 1f;

        if (Input.GetKey(KeyCode.LeftControl))
            verticalInput = -1f;
    }

    private void FixedUpdate()
    {
        Vector3 horizontalVelocity =
            transform.forward * currentSpeed;

        Vector3 verticalVelocity =
            Vector3.up * verticalInput * verticalSpeed;

        rb.linearVelocity =
            horizontalVelocity +
            verticalVelocity;

        currentSpeed *= drag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bateu em: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Wall"))
        {
            if (health != null)
            {
                health.TakeDamage(10f);
            }

            Debug.Log("Bateu na parede");
        }
    }
}
