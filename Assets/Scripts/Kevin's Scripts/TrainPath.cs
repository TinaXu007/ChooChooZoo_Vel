using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPath : MonoBehaviour
{
    /* 0 - Represents path to the left 
     * 1 - Represents path to the right
     * 2 - Represents path to the north
     * 3 - Represents path to the south */
    public TrainPath[] connectedPaths;
    public TrainPoint[] points;

    private float trainY = 1f; // Y-level of the train
    private float[] distances;
    private Vector3[] forwardVectors;
    private float traveledDistance = 0f;
    private Quaternion[] rotations;
    private bool finishedPath = false; // Has this entire path been travelled before?

    public void InstantiateArrays()
    {
        forwardVectors = new Vector3[points.Length - 1];
        for (int i = 0; i < forwardVectors.Length; i++)
        {
            forwardVectors[i] = new Vector3(points[i + 1].GetLocation().x - points[i].GetLocation().x, 0f, points[i + 1].GetLocation().y - points[i].GetLocation().y);
        }

        distances = new float[points.Length - 1];
        for (int i = 0; i < forwardVectors.Length; i++)
        {
            distances[i] = Vector2.Distance(points[i].GetLocation(), points[i + 1].GetLocation());
            Debug.Log(distances[i]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public TrainPath GetLeft()
    {
        if (connectedPaths[0] != null)
        {
            return connectedPaths[1];
        }
        else
        {
            Debug.Log("No path connected");
            return null;
        }
    }

    public bool NotThereYet(int inputStage, bool arcMovement, Vector2 vehiclePos)
    {
        return (traveledDistance < distances[inputStage]);
        if (arcMovement)
        {
            return Vector2.Distance(vehiclePos, GetPoint(inputStage + 1).GetLocation()) < 0.01f;
        }
        else
        {
            return (traveledDistance < distances[inputStage]);
        }
    }

    public TrainPath GetRight()
    {
        if (connectedPaths[1] != null)
        {
            return connectedPaths[1];
        }
        else
        {
            Debug.Log("No path connected");
            return null;
        }
    }

    public Quaternion GetRotation(int inputStage)
    {
        return rotations[inputStage];
    }

    public Vector3 GetForwardVector(int inputStage)
    {
        return forwardVectors[inputStage];
    }

    public void SetTraveledDistance(float input)
    {
        traveledDistance = input;
    }

    public float GetTraveledDistance()
    {
        return traveledDistance;
    }

    public int GetPointsLength()
    {
        return points.Length;
    }

    public bool GetFinishedPath()
    {
        return finishedPath;
    }

    public void SetPoints(TrainPoint[] input)
    {
        points = input;
    }

    public Vector3 GetFirstLocation() {        
        return new Vector3(points[0].GetLocation().x, trainY, points[0].GetLocation().y);
    }

    public TrainPoint GetPoint(int index)
    {
        return points[index];
    }
}
