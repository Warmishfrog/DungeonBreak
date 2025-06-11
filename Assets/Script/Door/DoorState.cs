using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorState
{    
    public DoorState(DoorState _door, DoorStateMachine _stateMachine, Animator _animationController, string _animationName) 
    {
        door = _door;
        stateMachine = _stateMachine;
        animationController = _animationController;
        animationName = _animationName;
    }

    protected DoorState door;
    protected DoorStateMachine stateMachine;
    protected Animator animationController;
    protected string animationName;

    protected bool isExitingState;
    protected bool isAnimationFinished;
    protected float startTime;

    public virtual void Enter()
    {
        isAnimationFinished = false;
        isExitingState = false;
        startTime = Time.time;
        animationController.SetBool(animationName, true);
    }

    public virtual void Exit()
    {
        isExitingState = true;
        if (!isAnimationFinished) isAnimationFinished = true;
        animationController.SetBool(animationName, false);
    }

    public virtual void LogicUpdate()
    {
        TransitionChecks();
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void TransitionChecks()
    {
    }

    public virtual void AnimationTrigger()
    {
        isAnimationFinished = true;
    }
}
