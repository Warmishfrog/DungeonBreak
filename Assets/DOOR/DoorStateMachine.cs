using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class DoorStateMachine : MonoBehaviour
{
    protected Animator Animator { get; private set; }

    [SerializeField ]public IState CurrentState;
    public DoorOpenState DoorOpenState;
    public DoorCloseState DoorCloseState;

    //public static DoorStateMachine Instance;

    private void Awake()
    {
        DoorOpenState = new DoorOpenState();
        DoorCloseState = new DoorCloseState();
    }

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit(this);        
        CurrentState = newState;
        CurrentState.Enter(this);
    }
}

public interface IState
{
    void Enter(DoorStateMachine doorStateMachine);
    void Exit(DoorStateMachine doorStateMachine);
    void UpdateState(DoorStateMachine doorStateMachine);
}