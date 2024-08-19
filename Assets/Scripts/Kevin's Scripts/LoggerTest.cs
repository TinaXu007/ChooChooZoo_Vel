using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Text;
using System.IO;
using TMPro;
using System.Linq;
using System;
// Credit to Luis Mesias Flores for the original Logger class structure and usage of StreamWriter

public class LoggerTest : MonoBehaviour
{
    public String sceneName;    

    private StringBuilder currentData; // All the data that has been stored so far, appended to 1 really big stringbuilder
    private StreamWriter file;
    private bool timestampEnabled = false; // If true, puts timestamp at the beginning of each line
    private string trialLogFile;
    private int logFileCounter = 0;

    private string headerStatement = "Beginning of the log file"; // Top line of the log file

    // Start is called before the first frame update
    void Start()
    {
        currentData = new StringBuilder(); // Initialize the data collector
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SaveLogFile();
        }
    }

    // Made public in order to let other classes control the saving of this log file
    public void SaveLogFile()
    {
        StartCoroutine(logTrial());
        UnityEngine.Debug.Log("Logging");
    }

    // Create the file and store all of the contents of currentData into it
    private IEnumerator logTrial()
    {
        try
        {
            // create log file if it does not already exist. Otherwise open it for appending new trial
            if (!File.Exists(trialLogFile))
            {
                trialLogFile = "Log_" + sceneName + System.String.Format("{0:_yyyy_MM_dd_hh_mm_ss}", System.DateTime.Now) + ".txt";
                file = new StreamWriter(trialLogFile);
                file.WriteLine(headerStatement); 
            }
            else
            {
                file = File.AppendText(trialLogFile);
                logFileCounter++;
            }
            file.WriteLine(currentData.ToString());
            file.Close();
            currentData = new StringBuilder(); // Reset the data
            UnityEngine.Debug.Log("FinishedLogging");
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.Log("Error in accessing file: " + e);
        }
        yield return new WaitForSeconds(.1f);
    }
        
    // Labels - Any descriptor of the data you would like to put E.g. The question text
    // Data - The data e.g. The user resonse to a question
    public void LogData(string[] labels, string[] data)
    {
        if (timestampEnabled) currentData.Append(Time.time); // Add the timestamp at beginning if needed
        if (labels != null && labels.Length > 0)
        {
            for (int i = 0; i < currentData.Length; i++) currentData.Append(labels[i].ToString());
            currentData.AppendLine();
        }
        for(int j = 0; j < currentData.Length; j++) currentData.Append(data[j].ToString());
        currentData.AppendLine();
    }
}