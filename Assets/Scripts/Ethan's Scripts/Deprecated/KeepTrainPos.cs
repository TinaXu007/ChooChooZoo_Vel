using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class KeepTrainPos : MonoBehaviour
{
    private string csvFileName = "train_position.csv";
    public GameObject Train;
    private Vector3 trainPosition;

    private void Start()
    {
        trainPosition = Train.transform.position;
        // Load the train position from the CSV file (if it exists)
        LoadTrainPosition();
    }

    private void Update()
    {
        // Update the train position as needed (e.g., user-controlled movement)
        trainPosition = Train.transform.position;

        // Save the train position to the CSV file
        SaveTrainPosition();
    }

    private void SaveTrainPosition()
    {
        // Create or overwrite the CSV file
        StreamWriter writer = new StreamWriter(Path.Combine(Application.dataPath, csvFileName));
        Debug.Log(Application.dataPath);

        // Write the current train position to the CSV file
        writer.WriteLine(trainPosition.x + "," + trainPosition.y + "," + trainPosition.z);

        // Close the file
        writer.Close();
    }

    private void LoadTrainPosition()
    {
        // Check if the CSV file exists
        if (File.Exists(Path.Combine(Application.dataPath, csvFileName)))
        {
            // Read the train position from the CSV file
            string[] lines = File.ReadAllLines(Path.Combine(Application.dataPath, csvFileName));
            string[] values = lines[0].Split(',');
            float x = float.Parse(values[0]);
            float y = float.Parse(values[1]);
            float z = float.Parse(values[2]);

            // Set the train's position
            trainPosition = new Vector3(x, y, z);
            Train.transform.position = trainPosition;
        }
    }
}
