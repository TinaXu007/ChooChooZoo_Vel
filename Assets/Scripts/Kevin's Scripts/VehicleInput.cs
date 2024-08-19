using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInput : MonoBehaviour
{
    private int leftOrRight; // 1 = right; 2 = left;
    private bool waitingForInput = false;
    private bool pause; // If true, tells vehicle controller to stop the vehicle

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingForInput)
        {
            if (Input.GetAxis("Horizontal") > 0.5)
            {
                leftOrRight = 1;
                waitingForInput = false;
                pause = false;
            }
            else if (Input.GetAxis("Horizontal") < -0.5)
            {
                leftOrRight = 2;
                waitingForInput = false;
                pause = false;
            }
        }
        else
        {
            leftOrRight = 0; // No input
        }
    }

    // Called when user enters intersection. Prompts this script to start reading for a user input
    public void PromptDirection()
    {
        waitingForInput = true;
        pause = true;
    }

    // Put in adaptive controls for this
    public int GetLeftOrRight()
    {
        return leftOrRight;
    }

    public bool GetWaitingForInput()
    {
        return waitingForInput; 
    }

    public bool GetPause()
    {
        return pause;
    }
}
