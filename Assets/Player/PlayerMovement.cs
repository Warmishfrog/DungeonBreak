using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]    
    private float moveSpeed;
    [SerializeField] private float walkSpeed= 7f;
    [SerializeField] private float sprintSpeed = 10f;

    [SerializeField] private float groundDrag = 4f;

    [SerializeField] private float maxBunnyhopSpeed = 14f;
    [SerializeField] private float bunnyhopAcceleration = 1.1f; // multiplier

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpCooldown = 0.25f;
    [SerializeField] private float airMultiplier = 0.4f; // Multiplier for speed in the air
    bool readyToJump = true;

    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Grounded Check")]
    public float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool grounded;
    [SerializeField] private bool grounded2;
    Vector3 groundCheckPosition; // Optional: Transform to visualize ground check
    [SerializeField] private float groundCheckDistance = 0.3f; // Distance for ground check

    [Header("Slope Handling")]
    public float maxSlopeAngle = 45f; // Maximum slope angle the player can walk on
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [SerializeField] private Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    public MovementState state = default;

    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent the Rigidbody from rotating

        
    }

    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        groundCheckPosition = transform.position + Vector3.down;
        grounded2 = Physics.CheckSphere(groundCheckPosition, groundCheckDistance, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        //handle drag 
        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0f; // No drag when in the air
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void StateHandler()
    {
        //mode - sprinting
        if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        
        //mode - walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        //mode - air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //onslope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0f) 
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        Vector3 force = OnSlope() && !exitingSlope
            ? GetSlopeMoveDirection() * moveSpeed * 20f
            : moveDirection.normalized * moveSpeed * 10f * (grounded ? 1f : airMultiplier);

        rb.AddForce(force, ForceMode.Force);

        rb.useGravity = !OnSlope(); // Disable gravity when on slope to prevent sliding down
    }

    private void SpeedControl()
    {
        //limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)

                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        //limiting speed on ground or in-air
        if (grounded)
        {

            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        //bunnyhopping
        else 
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > maxBunnyhopSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * maxBunnyhopSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    #region jumping

    private void Jump()
    {
        exitingSlope = true;


        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.velocity = horizontalVelocity + Vector3.up * jumpForce;


        float currentSpeed = horizontalVelocity.magnitude;
        if (currentSpeed < maxBunnyhopSpeed)
        {
            float newSpeed = Mathf.Min(currentSpeed * bunnyhopAcceleration, maxBunnyhopSpeed);
            Vector3 newHorizontalVelocity = horizontalVelocity.normalized * newSpeed;
            rb.velocity = newHorizontalVelocity + Vector3.up * rb.velocity.y;
        }

        //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset y velocity before jumping

        //rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false; // Reset exiting slope state after jump
    }

    #endregion

    #region slope handling

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
    #endregion

    /// <summary>
    /// debug ground check sphere in the editor
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPosition, groundCheckDistance);
    }
}

