using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera_Focus : MonoBehaviour
{
    public Transform Locomotive;


    public Camera mainCamera;
    public Transform target; // Reference to the target GameObject
    public float focusDistance = 25f; // Distance threshold for focusing
    public GameObject Train;

    void Start()
    {
        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //// Toggle view fixation when pressing left alt.
        //if (Input.GetKeyDown(KeyCode.LeftAlt))
        //{
        //    isViewFixed = !isViewFixed;

        //    // Unlock the cursor if the view is not fixed.
        //    Cursor.lockState = isViewFixed ? CursorLockMode.Locked : CursorLockMode.None;
        //    //Cursor.visible = !isViewFixed;
        //}
        // Collect mouse input.
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
            Debug.LogWarning("Look at target");
            mainCamera.transform.LookAt(target);
        }
    }
}
