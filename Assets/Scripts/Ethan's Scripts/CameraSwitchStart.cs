using UnityEngine;

public class CameraSwitchStart : MonoBehaviour
{
    public Camera firstCamera;
    public Camera secondCamera;
    public float transitionDuration = 2.0f; // Duration of the transition.

    private bool isSwitching = false;
    private float transitionStartTime;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Vector3 OriginalPosition;  //camera 2 original position and rotation
    private Quaternion OriginaRotation;

    private void Start()
    {
        secondCamera.enabled = false;
        OriginalPosition = secondCamera.transform.position;
        OriginaRotation = secondCamera.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train") && !isSwitching)
        {

            // Enable the second camera.
            secondCamera.enabled = true;

            // Disable the first camera.
            firstCamera.enabled = false;

            // Store the initial camera position and rotation.
            initialPosition = firstCamera.transform.position;
            initialRotation = firstCamera.transform.rotation;

            // Start the camera switch transition.
            isSwitching = true;
            transitionStartTime = Time.time;
        }
    }

    private void Update()
    {
        if (isSwitching)
        {
            // Calculate the lerp factor based on time.
            float t = (Time.time - transitionStartTime) / transitionDuration;

            // Lerp camera position and rotation from the initial position/rotation to the second camera's position/rotation.
            secondCamera.transform.position = Vector3.Lerp(initialPosition, OriginalPosition, t);
            secondCamera.transform.rotation = Quaternion.Slerp(initialRotation, OriginaRotation, t);

            // If the transition is complete, stop switching and reset the first camera's position/rotation.
            //if (t >= 1.0f)
            //{
            //    isSwitching = false;
            //    firstCamera.transform.position = secondCamera.transform.position;
            //    firstCamera.transform.rotation = secondCamera.transform.rotation;
            //}
        }
    }
}
