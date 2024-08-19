using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Focus : MonoBehaviour
{
    public Transform target; // Reference to the target GameObject
    public float focusDistance = 25f; // Distance threshold for focusing

    public GameObject Train;
    private Camera mainCamera;

    private void Start()
    {
        // Get the main camera in the scene
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (target == null || mainCamera == null)
        {
            // Ensure that the target and mainCamera are assigned
            Debug.LogWarning("Target or Main Camera not assigned.");
            return;
        }

        // Calculate the distance between the camera and the target
        float distanceToTarget = Vector3.Distance(Train.transform.position, target.position);

        // Check if the camera is within the focus distance range
        if (distanceToTarget <= focusDistance)
        {
            // Set the camera's look-at position to the target position
            mainCamera.transform.LookAt(target);
        }
    }
}
