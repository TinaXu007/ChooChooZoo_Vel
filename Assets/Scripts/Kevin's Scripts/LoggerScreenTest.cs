using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;
using UnityEngine.InputSystem;

public class LoggerScreenTest : MonoBehaviour
{
    public string logName;
    public string sceneName;
    public AutoScroll autoScroll;

    private StringBuilder currentData; // All the data that has been stored so far, appended to 1 really big stringbuilder
    private StreamWriter file;
    private bool timestampEnabled = false; // If true, puts timestamp at the beginning of each line
    private string trialLogFile;
    private int logFileCounter = 0;
    private bool isButtonHeld; // Frame signal for the button hold (deprecated)
    private float buttonHoldStartTime; // Used for measuring how long the spacebar has been pressed
    private float buttonHoldEndTime;
    private float[] buttonHoldStartTimes; // Used for measuring how many times any non-spacebar button has been pressed (can be configured by user)
    private float[] buttonHoldEndTimes;
    private int totalButtonPresses; // Tracks how many times the user pressed the spacebar
    private int[] otherButtonPresses; // Tracks how many times non-spacebar buttons were pressed
    private const int numInputs = 5; // Placeholder for the length of the arrays


    private string headerStatement; // Top line of the log file

    // Start is called before the first frame update
    void Start()
    {
        headerStatement = "Beginning of the log file for Scene: " + sceneName;
        totalButtonPresses = 0;
        currentData = new StringBuilder();
        buttonHoldStartTimes = new float[numInputs];
        buttonHoldEndTimes = new float[numInputs];
        otherButtonPresses = new int[numInputs];
        LogData(new string[] { "Time, ButtonType, ButtonPressDuration" }, null); // Log the labels
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            SaveLogFile();
        }

        // Maybe change to the InputManager later, if I figure out how that works
        // Record time at the beginning of key press
        if (Input.GetKeyDown("space"))
        {
            buttonHoldStartTime = Time.time;
            totalButtonPresses++;
            LogData(null, new string[] { buttonHoldStartTime.ToString(), ",Space,," }); // Log current time and when it's pressed
        }
        // Then log the duration of press when it's released
        if (Input.GetKeyUp("space"))
        {
            buttonHoldEndTime = Time.time;
            LogData(null, new string[] { buttonHoldEndTime.ToString(), ",Space,", (buttonHoldEndTime - buttonHoldStartTime).ToString(), "," }); // Log current time and when it's pressed and the duration
            if (Mathf.Abs(buttonHoldEndTime - buttonHoldStartTime) > ScreenTestData.threshold) { ScreenTestData.SaveButtonPress(buttonHoldStartTime, buttonHoldEndTime - buttonHoldStartTime, "spacebar"); }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ButtonDownLog(0, "DownArrow");
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            ButtonReleasedLog(0, "DownArrow");
        }
    }


    public void ButtonDownLog(int index, string otherButtonName)
    {
        buttonHoldStartTimes[index] = Time.time;
        otherButtonPresses[index]++;
        LogData(null, new string[] { buttonHoldStartTimes[index].ToString(), ",", otherButtonName, ",," }); // Log current time and when it's pressed
    }

    public void ButtonReleasedLog(int index, string otherButtonName)
    {
        buttonHoldEndTimes[index] = Time.time;
        LogData(null, new string[] { buttonHoldEndTimes[index].ToString(), ",", otherButtonName, ",", (buttonHoldEndTimes[index] - buttonHoldStartTimes[index]).ToString(), "," }); // Log current time and when it's pressed and the duration
        if (Mathf.Abs(buttonHoldEndTimes[index] - buttonHoldStartTimes[index]) > ScreenTestData.threshold) { ScreenTestData.SaveButtonPress(buttonHoldStartTimes[index], buttonHoldEndTimes[index] - buttonHoldStartTimes[index], "downarrow"); }
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
            LogData(new string[] { "Number of Button Presses" }, null); // Label 
            LogData(new string[] { "Spacebar" }, new string[] { totalButtonPresses.ToString() });
            LogData(new string[] { "Down Arrow" }, new string[] { otherButtonPresses[0].ToString() });
            // create log file if it does not already exist. Otherwise open it for appending new trial
            if (!File.Exists(trialLogFile))
            {
                trialLogFile = "Log_" + logName + System.String.Format("{0:_yyyy_MM_dd_hh_mm_ss}", System.DateTime.Now) + ".txt";
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
            currentData = new StringBuilder();
            UnityEngine.Debug.Log("FinishedLogging");
            //Debug.Log(ScreenTestData.OutputButtonDataString());
            // Debugging
            //autoScroll.setDelay((int)ScreenTestData.GetAveragePressDuration());
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
            for (int i = 0; i < labels.Length; i++) { currentData.Append(labels[i].ToString()); }
            currentData.AppendLine();
        }
        if (data != null)
        {
            for (int j = 0; j < data.Length; j++) { currentData.Append(data[j].ToString()); }
            currentData.AppendLine();
        }
    }
}
