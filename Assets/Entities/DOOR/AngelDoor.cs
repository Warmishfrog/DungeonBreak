using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelDoor : MonoBehaviour
{

    [Header("Initialisation")]
    [SerializeField] private DoorStateMachine doorStateMachine;
    [SerializeField] private Transform Player;
    [SerializeField] private Camera Camera;
    [SerializeField] private Collider AngelCollider;

    [Header("Settings")]
    [SerializeField] private float AnimatorSpeed = 0.3f;
    [SerializeField] private float maxDistance = 20f; // Maximum distance to check visibility
    [SerializeField] private float stateChangeDelay = 0.75f; // seconds

    [Header("Debug")]
    [SerializeField] private bool IsComplete = false;
    [SerializeField] private bool IsVisible;
    [SerializeField] private bool currentShouldClose;
    [SerializeField] private bool previousShouldClose = false;
    [SerializeField] private float stateChangeTimer = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        doorStateMachine.Animator.speed = AnimatorSpeed;

        doorStateMachine.ChangeState(doorStateMachine.DoorOpenState); // Initialize with the door closed state
        stateChangeTimer = stateChangeDelay; // Initialize the timer

        AngelCollider.isTrigger = true; // Ensure the enter collider is a trigger to avoid physics interactions
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsComplete) return;

        AngelLogic();        
    }

    public void AngelLogic()
    {
        IsVisible = GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera), AngelCollider.bounds);

        float distanceToPlayer = Vector3.Distance(Player.position, transform.position);
        bool withinDistance = distanceToPlayer <= maxDistance;

        currentShouldClose = IsVisible && withinDistance;

        if (stateChangeTimer > 0f)
        {
            stateChangeTimer -= Time.fixedDeltaTime;
        }

        // Only commit to changing state if timer has elapsed
        if (stateChangeTimer <= 0f && currentShouldClose != previousShouldClose)
        {
            previousShouldClose = currentShouldClose;
            doorStateMachine.ChangeState(currentShouldClose ? doorStateMachine.DoorCloseState : doorStateMachine.DoorOpenState);
            stateChangeTimer = stateChangeDelay;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IsComplete = true;   
        stateChangeTimer = 0f; // Reset the timer to prevent further state changes
        doorStateMachine.Animator.speed = 1f;
        doorStateMachine.ChangeState(doorStateMachine.DoorOpenState);

    }

}
