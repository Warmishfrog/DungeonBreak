using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockedState : IState
{
    public void Enter(DoorStateMachine doorStateMachine, Animator animator)
    {
    }

    public void Exit(DoorStateMachine doorStateMachine)
    {
    }

    public void Unlocked(DoorStateMachine doorStateMachine)
    {
        //delete gameobject
        //change state to DoorCloseState
        Debug.Log("Door is now unlocked.");
        doorStateMachine.ChangeState(doorStateMachine.DoorCloseState);
        //lockObj.SetActive(false); // Assuming lockObj is the GameObject representing the lock
    }

    public void UpdateState(DoorStateMachine doorStateMachine)
    {
        doorStateMachine.ChangeState(doorStateMachine.DoorCloseState);
    }
}
