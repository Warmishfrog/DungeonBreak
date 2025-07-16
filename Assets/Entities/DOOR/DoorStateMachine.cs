using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class DoorStateMachine : MonoBehaviour
{
    public Animator Animator;

    [SerializeField ]public IState CurrentState;

    public DoorOpenState DoorOpenState;
    public DoorCloseState DoorCloseState;
    public DoorLockedState DoorLockedState;

    //public static DoorStateMachine Instance;

    private void Awake()
    {
        DoorOpenState = new DoorOpenState();
        DoorCloseState = new DoorCloseState();
        DoorLockedState = new DoorLockedState();
    }

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit(this);        
        CurrentState = newState;
        CurrentState.Enter(this, Animator);
    }
}

public interface IState
{
    void Enter(DoorStateMachine doorStateMachine, Animator animator);
    void Exit(DoorStateMachine doorStateMachine);
    void UpdateState(DoorStateMachine doorStateMachine);
}