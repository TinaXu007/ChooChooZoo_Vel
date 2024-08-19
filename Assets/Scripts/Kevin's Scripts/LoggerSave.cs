using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

// Outputs text version of the stored data when application closes

public class LoggerSave : MonoBehaviour
{
    private StreamWriter file;
    private string trialLogFile;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug: Output all the current data when p is pressed
        if (Input.GetKeyDown("p"))
        {
            PrintFile();
        }
    }

    // When the game closes, tell the logger to save everything
    private void OnApplicationQuit()
    {
        PrintFile();
    }

    // Print log file
    private void PrintFile()
    {
        BigLogger.SaveCurrentData();
        trialLogFile = "Log_ChooChooTrain_" + System.String.Format("{0:_yyyy_MM_dd_hh_mm_ss}", System.DateTime.Now) + ".txt";
        file = new StreamWriter(trialLogFile);
        file.WriteLine(BigLogger.GetAllData());
        file.Close();
    }
}
