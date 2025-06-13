using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    // Drag the required KeyItem asset here in the Inspector
    #nullable enable
    public KeyItem? requiredKey;
    #nullable disable

    public DoorStateMachine doorStateMachine;

    [SerializeField] public IState StartingState;

    public UserInterfaceText UIText;

    [SerializeField] private bool isClosable = true; // Whether the door can be closed by the player

    public void Start()
    {
        doorStateMachine.ChangeState(StartingState); // Initialize with the door closed state
    }

    /// <summary>
    /// This method would be called by player's interaction script
    /// </summary>
    public void Interact()
    {

        // Check the central inventory
        if (Inventory.instance.HasKey(requiredKey) || requiredKey == null)
        {
            // Add your door opening logic here (e.g., play animation, disable collider)
            GameObject doorParent = transform.parent.gameObject;
            Animator animator = doorParent.GetComponent<Animator>();

            if (doorStateMachine.CurrentState == doorStateMachine.DoorCloseState)
            {
                // Open the door
                string msg = "Key accepted. Opening door.";
                Debug.Log(msg);
                //UIText.SetText(msg);

                animator.SetTrigger("DoorOpen");

                doorStateMachine.ChangeState(doorStateMachine.DoorOpenState);
            }
            else if (doorStateMachine.CurrentState == doorStateMachine.DoorOpenState)
            {
                // Close the door
                string msg = "Closing door.";
                Debug.Log(msg);
                //UIText.SetText(msg);

                animator.SetTrigger("DoorClosed");

                doorStateMachine.ChangeState(doorStateMachine.DoorCloseState);
            }
        }
        else
        {
            string msg = $"This door requires the {requiredKey.keyName}.";
            Debug.Log(msg);
            UIText.SetText(msg);

            // Play a "locked" sound effect, show a message, etc.
        }
    }
}
