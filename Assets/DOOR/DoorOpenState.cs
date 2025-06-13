using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenState : IState
{

    public void Enter(DoorStateMachine doorStateMachine)
    {
        //play animation
        //play sound
        Debug.Log("Door is now open.");
        //doorStateMachine..SetTrigger("doorOpen"); // Assuming you have an "OpenDoor" trigger in your Animator

        
    }

    public void Exit(DoorStateMachine doorStateMachine)
    {
    }

    public void UpdateState(DoorStateMachine doorStateMachine)
    {
        doorStateMachine.ChangeState(doorStateMachine.DoorCloseState);
    }
}
