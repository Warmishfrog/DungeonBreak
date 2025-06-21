using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public DoorStateMachine doorStateMachine;
    public UserInterfaceText UIText;
    [SerializeField] private bool isClosable = true; 

    private ILockable doorLock;

    public void Start()
    {
        //TODO: serialize an option for starting state
        doorLock = GetComponent<ILockable>(); // Can be null if no lock

        if (doorLock != null && doorLock.IsLocked)        
            doorStateMachine.ChangeState(doorStateMachine.DoorLockedState);    
        
        else        
            doorStateMachine.ChangeState(doorStateMachine.DoorCloseState);
        
    }

    /// <summary>
    /// This method would be called by player's interaction script
    /// </summary>
    public void Interact()
    {
        if (doorLock != null && doorLock.IsLocked)
        {
            TryUnlock();
            return;
        }
        else
        {
            switch (doorStateMachine.CurrentState)
            {
                case DoorOpenState _:
                    // If the door is open, close it
                    doorStateMachine.ChangeState(doorStateMachine.DoorCloseState);
                    break;

                case DoorCloseState _:
                    // If the door is closed, open it
                    if (isClosable) doorStateMachine.ChangeState(doorStateMachine.DoorOpenState);
                    
                    break;
                default:
                    Debug.LogWarning("Unknown door state.");
                    break;
            }
        }
    }

    public bool TryUnlock()
    {
        if (doorLock == null)
            return true;

        bool unlocked = doorLock.TryUnlock();
        if (unlocked)
        {
            Debug.Log("Door unlocked.");
            UIText.ShowText("Door unlocked.");
            doorStateMachine.ChangeState(doorStateMachine.DoorOpenState);
        }
        else
        {
            string msg = doorLock.RequiredKey != null ?
                $"This door requires the {doorLock.RequiredKey.keyName}." :
                "This door can't be opened.";
            UIText.ShowText(msg);
        }

        return unlocked;

        /*
        // Check the central inventory
        if (Inventory.instance.HasKey(requiredKey))
        {
            // If the door is locked, unlock it
            Debug.Log("Door unlocked.");
            UIText.ShowText("Door unlocked.");

            isLocked = false;
            if (lockObj != null) lockObj.SetActive(false); // Assuming lockObj is the GameObject representing the lock

            doorStateMachine.ChangeState(doorStateMachine.DoorOpenState);            
            
            return true;
        }
        else
        {
            string msg = requiredKey != null ? $"This door requires the {requiredKey.keyName}.": "This door can't be opened.";
            Debug.Log(msg);
            UIText.ShowText(msg);
            return false;
        }
        //*/
    }

    public void EnableLock()
    {
        throw new System.NotImplementedException();
    }
}
