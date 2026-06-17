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

    private Rigidbody rb;
    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed += acceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            currentSpeed -= reverseAcceleration * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(
            currentSpeed,
            -maxSpeed * 0.5f,
            maxSpeed
        );

        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float turnInput = 0f;

            if (Input.GetKey(KeyCode.A))
                turnInput = -1f;

            if (Input.GetKey(KeyCode.D))
                turnInput = 1f;

            transform.Rotate(
                Vector3.up,
                turnInput * turnSpeed * Time.deltaTime
            );
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity =
            transform.forward * currentSpeed;

        currentSpeed *= drag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bateu em: " + collision.gameObject.name);
    }
}