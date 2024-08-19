using UnityEngine;
using System.Collections;

public class CameraSwitchEnd : MonoBehaviour
{
    public Camera cameraOne; // Assign Camera One (attached to the train)
    public Camera cameraTwo; // Assign Camera Two (fixed camera)

    public float transitionDuration = 2.0f; // Duration of the camera transition

    private Vector3 cameraTwoInitialPosition;
    private Quaternion cameraTwoInitialRotation;

    void Start()
    {
        // Store Camera Two's initial position and rotation
        cameraTwoInitialPosition = cameraTwo.transform.position;
        cameraTwoInitialRotation = cameraTwo.transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            StartCoroutine(SwitchCamera());
        }
    }

    IEnumerator SwitchCamera()
    {
        float elapsed = 0.0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionDuration;

            // Interpolate position and rotation from Camera Two to Camera One
            cameraTwo.transform.position = Vector3.Lerp(cameraTwoInitialPosition, cameraOne.transform.position, t);
            cameraTwo.transform.rotation = Quaternion.Lerp(cameraTwoInitialRotation, cameraOne.transform.rotation, t);

            yield return null;
        }

        // Enable Camera One and disable Camera Two at the end of the transition
        cameraOne.enabled = true;
        cameraTwo.enabled = false;

        // Restore Camera Two's original position and rotation
        cameraTwo.transform.position = cameraTwoInitialPosition;
        cameraTwo.transform.rotation = cameraTwoInitialRotation;
    }
}