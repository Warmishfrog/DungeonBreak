using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class DoorStateMachine : MonoBehaviour
{
    protected Animator Animator { get; private set; }

    public IState CurrentState { get; private set; }
    public DoorOpenState DoorOpenState { get; private set; }
    public DoorCloseState DoorCloseState { get; private set; }

    private void Awake()
    {

        DoorOpenState = new DoorOpenState();
        DoorCloseState = new DoorCloseState();

        CurrentState = DoorCloseState;
    }

    private void Start()
    {
        ChangeState(DoorCloseState); // Initialize with the door closed state
    }

    void Update()
    {
        CurrentState?.UpdateState(this);        
    }

    public void ChangeState(IState newState)
    {
        if (CurrentState is not null)
        {
            CurrentState.Exit(this);
        }
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