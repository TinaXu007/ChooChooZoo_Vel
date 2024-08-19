using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotive_FPV_V2 : MonoBehaviour
{
    //public float mouseSensitivity = 2f;
    //private float cameraVerticalRotation = 0f;
    //private float cameraHorizontalRotation = 0f;
    //private bool isViewFixed = false;

    public Camera mainCamera;
    public List<GameObject> targets; // Reference to the target GameObject
    public float [] distanceToTarget;
    [SerializeField] float focusDistance = 25f; // Distance threshold for focusing
    public GameObject Train;
    private Quaternion Camera_rotation;
    private int i;
    private Transform currentTarget;

    void Start()
    {
        i = targets.Count;
        Camera_rotation = mainCamera.transform.localRotation;
        distanceToTarget = new float[targets.Count];
        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Vector3 currentTrainRotation = Train.transform.rotation.eulerAngles;

        //// Set the Z-axis rotation to 0
        //currentTrainRotation.z = 0;

        //// Apply the modified rotation back to the GameObject
        //Train.transform.rotation = Quaternion.Euler(currentTrainRotation);


        for (i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null || mainCamera == null)
            {
                // ensure that the target and maincamera are assigned
                Debug.LogWarning("target or main camera not assigned.");
                return;
            }
            else
            {
                distanceToTarget[i] = Vector3.Distance(Train.transform.position, targets[i].transform.position);
            }

        }

        if (Train == null || targets.Count == 0)
            return;

        // Find the closest target within the trigger distance
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (GameObject target in targets)
        {
            if (target == null)
                continue;

            float distance = Vector3.Distance(Train.transform.position, target.transform.position);

            if (distance <= focusDistance && distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target.transform;
                //Debug.Log(target);
            }
        }

        // Focus the main camera on the closest target (if any)
        if (closestTarget != null)
        {
            //Debug.Log("look at target");
            //Debug.Log("closestTarget  " + closestTarget);
            currentTarget = closestTarget;
            mainCamera.transform.LookAt(closestTarget);
        }
        else
        {
            mainCamera.transform.localRotation = Camera_rotation;
            float trainYaw = Train.transform.rotation.eulerAngles.y;
            float trainPitch = Train.transform.rotation.eulerAngles.x;
            float trainRoll = Train.transform.rotation.eulerAngles.z;
            Quaternion newRotation = Quaternion.Euler(trainPitch, trainYaw, 0);
            mainCamera.transform.rotation = newRotation;
        }

    }
}
