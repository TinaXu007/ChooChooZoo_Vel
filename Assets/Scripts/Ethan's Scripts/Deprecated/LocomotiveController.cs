using UnityEngine;

public class LocomotiveController : MonoBehaviour
{
    public float accelerationForce = 10f;  // Acceleration force.
    public float maxSpeed = 5f;            // Maximum speed.
    public float decelerationForce = 2f;   // Deceleration force.
    public KeyCode accelerateKey = KeyCode.W;  // Key to accelerate.
    public KeyCode brakeKey = KeyCode.S;      // Key to brake/stop.
    private Rigidbody rb;
    private float currentSpeed = 0f;

    private void Start()
    {
        // Get the Rigidbody component attached to the GameObject.
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check for acceleration input.
        if (Input.GetKey(accelerateKey))
        {
            // Apply forward acceleration.
            Accelerate();
        }
        //else if (currentSpeed > 0f)
        //{
        //    // Apply deceleration force to gradually slow down when not accelerating.
           
        //}

        // Check for braking input.
        if (Input.GetKey(brakeKey))
        {
            // Stop the locomotive.
            Decelerate();
        }
    }

    private void Accelerate()
    {
        // Check if the locomotive is not exceeding the maximum speed.
        if (currentSpeed < maxSpeed)
        {
            // Apply forward force to accelerate.
            rb.AddForce(transform.forward * accelerationForce, ForceMode.Acceleration);
            currentSpeed = rb.velocity.magnitude;
        }
    }

    private void Decelerate()
    {
        // Apply deceleration force to gradually slow down.
        rb.AddForce(-rb.velocity.normalized * decelerationForce, ForceMode.Acceleration);
        currentSpeed = rb.velocity.magnitude;
    }

    private void StopLocomotive()
    {
        // Reset the locomotive's velocity to stop it.
        rb.velocity = Vector3.zero;
        currentSpeed = 0f;
    }
}
