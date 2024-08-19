using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public static class ScreenTestData 
{
    public const float threshold = 0.00001f;

    private static string savedData;
    private static List<ButtonData> buttonPresses = new List<ButtonData>();
    private static int[] numButtonPresses = new int[2]; // Length 2, for spacebar and down arrow key
    private static float averagePressDuration = 0f; // Average time each button was held

    // Store data
    // Called in LoggerScreenTest
    public static void SaveButtonPress(float timestamp, float pressDuration, string buttonType)
    {
        buttonPresses.Add(new ButtonData(timestamp, pressDuration, buttonType));
    }

    public static void SaveNumButtonPresses(int[] input)
    {
        numButtonPresses[0] = input[0]; // Spacebar number
        numButtonPresses[1] = input[1]; // Down arrow number
    }

    // Output methods 
    // Other classes can call these and then store the returned values in their own data structures or output them to UI/console
    public static List<ButtonData> OutputButtonData()
    {
        return buttonPresses;
    }

    // Debugging purposes
    public static string OutputButtonDataString()
    {    
        StringBuilder b = new StringBuilder();
        foreach(ButtonData datapiece in buttonPresses)
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

    public static int[] OutputNumButtonPresses()
    {
        return numButtonPresses;
    }

    public static float GetAveragePressDuration()
    {
        averagePressDuration = 0f;
        int counter = 0;
        foreach (ButtonData buttonData in buttonPresses)
        {
            if (buttonData.pressDuration != 0f) { averagePressDuration += buttonData.pressDuration; counter++; }
        }
        averagePressDuration /= counter; // Calculate user response as the average of how long they held the button

        return averagePressDuration;
    }
}