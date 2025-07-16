using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorStateEnum
{
    Open,
    Closed,
}


public class Test : MonoBehaviour
{
    public Dictionary<DoorStateEnum, DoorStateMachine> doorStateMachines;

}