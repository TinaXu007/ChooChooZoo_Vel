using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VehicleController : MonoBehaviour
{
    public VehicleInput vehicleInput; // Pull the adaptive control inputs from this script
    //public CinemachinePath[] paths; // Stores potential routes in the map
    public CinemachineDollyCart train; // User's train                                    
    public TrainPath[] paths; // Index 0 will always be the on-rails path (no control).  

    private Transform vehicleT;
    // I changed my mind let's not use an array, some kind of map data structure would be more preferable. I will implement it later
    private Vector2[] routes = new Vector2[3]; // x represents the rotation on the y axis that the vehicle should be in, y represents the distance it should travel
    private int routeIndex; // Counter variable for the route arrays
    private int movementType; // 0 - On rails; 1 - Basic left or right; 2 - Free movement
    private int bidirecStage;
    private int pointStage; // Represents the point selected in TrainPath
    private float speed = 2f;
    private float pathAngle; 
    private float distanceTraveled;
    private float smooth = 10f; // Turn speed
    private bool pathFinished = false; // Used for on-rail movement
    private bool isVehicleStopped = false;
    private bool enabled = false; // Set true by the manager
    private bool arcMovement = false;
    private Transform target;
    private Vector3 targetPos;
    private Vector2 xypos;
    private TrainPath currPath;


    // Start is called before the first frame update
    void Start()
    {
        vehicleT = this.gameObject.transform;

        routeIndex = 0;
        bidirecStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            switch (movementType)
            {
                case 0: // On rails movement
                    if (!isVehicleStopped)
                    {
                        FollowPath(currPath);
                    }
                    break;
                case 1: // Basic movement
                    if (vehicleInput.GetLeftOrRight() > 0)
                    {
                        if (vehicleInput.GetLeftOrRight() == 1) // Temporary, we need to find a better way of calculating the next path index
                        {
                            //SetPath(paths[1]);
                        }
                        else if (vehicleInput.GetLeftOrRight() == 2)
                        {
                            //SetPath(paths[2]);
                        }
                    }
                    if (vehicleInput.GetPause())
                    {
                        train.m_Speed = 0f;
                    }
                    else
                    {
                        train.m_Speed = speed;
                    }
                    break;
                case 2: // Free movement
                    break;
            }
        }
    }

    public void SetMovementType(int typeOfMovement)
    {
        enabled = true;
        movementType = typeOfMovement;
        switch (movementType)
        {
            case 0:
                currPath = paths[0]; // Default to the 0 index when it's on rails movement
                currPath.InstantiateArrays();
                NextPath(currPath);
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    // Checks if this path is a "stop" or a normal path which you move along
    private void NextPath(TrainPath input)
    {        
        pointStage = 0;
        this.gameObject.transform.position = input.GetFirstLocation();
    }

    /* public void SetPath(CinemachinePath newPath)
    {
        train.m_Path = newPath;
        train.SetCartPosition(0f);
    } */

    public void FollowPath(TrainPath currPath)
    {
        if (currPath.GetTraveledDistance() <= 0.01f)
        {
            //vehicleT.rotation = currPath.GetRotation(pointStage);
            //currPath.SetTraveledDistance(0f);
            vehicleT.transform.forward = currPath.GetForwardVector(pointStage);            
        }
        if (arcMovement)
        {
            xypos.x = transform.position.x;
            xypos.y = transform.position.z;
        }
        // Move forward if we're not at the next point
        if (currPath.NotThereYet(pointStage, arcMovement, xypos))
        {
            if (arcMovement)
            {
                targetPos = target.position;
                // Rotate in arc
                SmoothLookAt(targetPos);
                // Move
                vehicleT.Translate(transform.forward * speed * Time.deltaTime);
            }
            else
            {
                vehicleT.Translate(new Vector3(0f, 0f, speed * Time.deltaTime));
            }         
            currPath.SetTraveledDistance(currPath.GetTraveledDistance() + speed * Time.deltaTime);
        }
        else if (pointStage < currPath.GetPointsLength())
        {
            arcMovement = false;
            pointStage++;
            currPath.SetTraveledDistance(0f);
            if (currPath.GetPoint(pointStage).GetComponent<TrainPathStop>())
            {
                StartCoroutine(StopVehicle(currPath.GetPoint(pointStage).GetComponent<TrainPathStop>().GetWaitTime())); // Placeholder time, might link this to a minigame
            }
            if (currPath.GetPoint(pointStage).GetComponent<TrainPathStop>())
            {
                target = currPath.GetPoint(pointStage + 1).gameObject.transform;
                arcMovement = true;
            }
            if (pointStage == currPath.GetPointsLength() - 1)
            {
                enabled = false; // Temporary
            }
        }
        
    }

    private void SmoothLookAt(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth);
    }

    private IEnumerator StopVehicle(float waitingTime)
    {
        isVehicleStopped = true;
        yield return new WaitForSeconds(waitingTime);
        isVehicleStopped = false;        
    }
}
