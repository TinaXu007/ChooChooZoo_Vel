using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    /* Basic state machine where the Update() loop continually checks certain object conditions and determines if the game should be advanced
     * to the next state based off that. StateChange() does the "setup" or the "instant actions" at the start of each state, whereas the 
     * Update() can read off continuous events/anticipate events that occur during each state. */
    public VehicleController vehicleController;
    
    private int state; // Current state of the state machine

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        StateChange(0);
        vehicleController.SetMovementType(0);
        vehicleController.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    public void StateChange(int nextState)
    {
        state = nextState;
        
        switch(state)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;                
        }
    }

    
}
