using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ButtonData
{
    public float timestamp;
    public float pressDuration;
    public string buttonType;

    public ButtonData(float timestamp, float pressDuration, string buttonType)
    {
        this.timestamp = timestamp;
        this.pressDuration = pressDuration;
        this.buttonType = buttonType;
    }

    public override string ToString()
    {
        StringBuilder b = new StringBuilder();
        b.Append(timestamp.ToString());
        b.Append(",");
        b.Append(buttonType);        
        b.Append(",");
        b.Append(pressDuration.ToString());
        b.AppendLine();
        return b.ToString();
    }
}