using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseState : IState
{
    public void Enter(DoorStateMachine doorStateMachine)
    {
        //play animation
        //play sound
        Debug.Log("Door is now closed.");
    }

    public void Exit(DoorStateMachine doorStateMachine)
    {
    }

    public void UpdateState(DoorStateMachine doorStateMachine)
    {
        doorStateMachine.ChangeState(doorStateMachine.DoorOpenState);
    }
}
