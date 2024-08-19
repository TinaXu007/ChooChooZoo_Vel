using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;

// Takes in data from all the possible minigames (and screen test) and records them into 1 big string
public static class BigLogger 
{
    public const float threshold = 0.00001f;

    private static string currentScene; // Name of the scene
    private static bool initialScene = true; // Checks if this is the first usage of this log. Flips to false anytime a minigame or scene test is loaded.
    private static StringBuilder currentData; // Data being collected in the scene that we are in
    private static StringBuilder allData = new StringBuilder();
    private static List<ButtonData> sceneButtonData = new List<ButtonData>();
    private static int[] numButtonPresses; // Length 2, for spacebar and down arrow key
    private static float averagePressDuration = 0f; // Average time each button was held
    //private static List<string> 

    public static void InitializeNewLogging(string sceneName)
    {
        if (!initialScene) // Save the data from previous scene
        {
            SaveCurrentData(); // Unity doesn't have an easy way of checking if we move from one scene to the next, so the data from the past scene is saved at the beginning of the new scene. 
        }
        else
        {
            initialScene = false;
        }
        currentScene = sceneName; // Set the new scene name
        currentData = new StringBuilder(); // Reset the string builder
        numButtonPresses = new int[] { 0, 0 }; // Reset number of button presses
    }

    public static void SaveCurrentData()
    {
        allData.Append(currentScene); // Append name of current scene
        allData.AppendLine();
        allData.Append("Time, ButtonType, ButtonPressDuration"); // Append the headers for button presses
        allData.AppendLine();
        //allData.Append(AggregateListButtonData(sceneButtonData));
        allData.Append(currentData.ToString()); // Append the data
        allData.AppendLine();
        allData.Append("Number of spacebar presses, Number of down arrow key presses");
        allData.AppendLine();
        allData.Append(numButtonPresses[0].ToString());
        allData.Append(",");
        allData.Append(numButtonPresses[1].ToString()); // Finished logging the number of button presses
        allData.AppendLine();
        allData.AppendLine(); // Create extra line of space for the next scene
    }

    /*private static string AggregateListButtonData(List<ButtonData> inputList)
    {
        StringBuilder b = new StringBuilder();
        foreach(ButtonData data in inputList)
        {
            b.Append(data.ToString());
            b.AppendLine();
        }
        return b.ToString();
    }*/

    // Store data
    // Called in minigame
    public static void SaveButtonPress(float timestamp, float pressDuration, string buttonType)
    {
        sceneButtonData.Add(new ButtonData(timestamp, pressDuration, buttonType));
        currentData.Append(sceneButtonData.Last().ToString()); // Fetch the button
        currentData.AppendLine();
    }

    public static void IncrementSpacebar()
    {
        numButtonPresses[0]++;
    }

    public static void IncrementDownArrow()
    {
        numButtonPresses[1]++;
    }

    public static void SaveNumsceneButtonData(int[] input)
    {
        numButtonPresses[0] = input[0]; // Spacebar number
        numButtonPresses[1] = input[1]; // Down arrow number
    }

    // Output methods 
    // Other classes can call these and then store the returned values in their own data structures or output them to UI/console
    public static List<ButtonData> OutputButtonData()
    {
        return sceneButtonData;
    }

    public static string GetAllData()
    {
        return allData.ToString();
    }

    // Debugging purposes
    public static string OutputButtonDataString()
    {
        StringBuilder b = new StringBuilder();
        foreach (ButtonData datapiece in sceneButtonData)
        {
            b.Append(datapiece.timestamp.ToString());
            b.Append(",");
            b.Append(datapiece.pressDuration.ToString());
            b.Append(",");
            b.Append(datapiece.buttonType);
            b.AppendLine();
        }
        return b.ToString();
    }

    public static int[] OutputNumsceneButtonData()
    {
        return numButtonPresses;
    }

    public static float GetAveragePressDuration()
    {
        averagePressDuration = 0f;
        int counter = 0;
        foreach (ButtonData buttonData in sceneButtonData)
        {
            if (buttonData.pressDuration != 0f) { averagePressDuration += buttonData.pressDuration; counter++; }
        }
        averagePressDuration /= counter; // Calculate user response as the average of how long they held the button

        return averagePressDuration;
    }

}