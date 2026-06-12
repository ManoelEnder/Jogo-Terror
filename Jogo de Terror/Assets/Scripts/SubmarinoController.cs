using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SubmarinoController : MonoBehaviour
{
    public float acceleration = 3f;
    public float maxSpeed = 8f;
    public float drag = 0.98f;
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
        currentSpeed += acceleration * Time.deltaTime;

    if (Input.GetKey(KeyCode.S))
        currentSpeed -= acceleration * Time.deltaTime;

    if (Input.GetKey(KeyCode.A))
        transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

    if (Input.GetKey(KeyCode.D))
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.right * currentSpeed;

        currentSpeed *= drag;
    }
}