using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenState : IState
{

    public void Enter(DoorStateMachine doorStateMachine, Animator animator)
    {
        // Open the door
        string msg = "Opening door.";
        Debug.Log(msg);
        //UIText.SetText(msg);

        animator.SetTrigger("DoorOpen");

        //TODO: play sound
    }

    public void Exit(DoorStateMachine doorStateMachine)
    {
    }

    public void UpdateState(DoorStateMachine doorStateMachine)
    {
        doorStateMachine.ChangeState(doorStateMachine.DoorCloseState);
    }
}
