using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseState : IState
{
    public void Enter(DoorStateMachine doorStateMachine, Animator animator)
    {
        //play animation
        //play sound
        // Close the door
        string msg = "Closing door.";
        Debug.Log(msg);
        //UIText.SetText(msg);

        animator.SetTrigger("DoorClosed");
    }

    public void Exit(DoorStateMachine doorStateMachine)
    {
    }

    public void UpdateState(DoorStateMachine doorStateMachine)
    {
        doorStateMachine.ChangeState(doorStateMachine.DoorOpenState);
    }
}
