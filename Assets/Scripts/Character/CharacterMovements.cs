using System;
using Unity.Collections;
using Unity.IntegerTime;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    // Input holders
    InputAction moveAction;
    InputAction jumpAction;

    public Rigidbody RBCharacter;

    [Header("Movements")]
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float deceleration = 60.0f;
    [SerializeField] private float acceleration = 50.0f;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float jumpBufferTime = 0.15f;

    [Header("Gravity")]
    [SerializeField] private float gravityFallMultiplier = 4.0f;
    [SerializeField] private float lowJumpGravityMultiplier = 4.0f;
    [SerializeField] private float baseMultiplier = 1.6f;
    [SerializeField] private float baseGravity = 9.81f;

    [Header("Ground Interactions")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask bodyLayer;
    [SerializeField] private Transform groundCheck; // feet of the player
    [SerializeField] private float groundCheckRadius = 0.2f;
    
    private float coyoteTimer;
    private float jumpBufferTimer;
    private bool isJumpHeld;


    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        RBCharacter.useGravity = false;

        Debug.Log("Character movements init");
    }

    void Update()
    {
        // Coyote timer
        if (IsGrounded())
        {
            coyoteTimer = coyoteTime;
        } 
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        // Jump buffer timer
        if (jumpAction.WasPressedThisFrame())
        {
            jumpBufferTimer = jumpBufferTime;
        } 
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        isJumpHeld = jumpAction.IsPressed();
    }

    void FixedUpdate()
    {
        HandleMovements();
        HandleJump();
        HandleGravity();
    }

    void HandleMovements()
    {
        float moveValue = moveAction.ReadValue<Vector2>().x;

        // What speed modifier should we apply ?
        float targetSpeed = moveValue * maxSpeed;
        float currentSpeedX = RBCharacter.linearVelocity.x;
        float speedDiff = targetSpeed - currentSpeedX;

        // If moving = move else : !moving = brake
        float rate;
        if (moveValue != 0)
        {
            rate = acceleration;
        } 
        else
        {
            rate = deceleration;
        }

        float movement = speedDiff * rate * Time.fixedDeltaTime;
        RBCharacter.AddForce(Vector3.right * movement, ForceMode.VelocityChange);
    }

    void HandleJump()
    {

        bool canJump = coyoteTimer > 0.0f && jumpBufferTimer > 0.0f;
        
        if (canJump)
        { 
            // Avoid bouncing
            Vector3 vec = RBCharacter.linearVelocity;
            vec.y = 0;            
            RBCharacter.linearVelocity = vec;

            RBCharacter.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            coyoteTimer = 0.0f;
            jumpBufferTimer = 0.0f;
        }

    }

    void HandleGravity()
    {
        float multiplier = baseMultiplier;

        if (RBCharacter.linearVelocity.y < 0.0f)
        {
            multiplier = gravityFallMultiplier;
        } 
        else if (RBCharacter.linearVelocity.y > 0.0f && !isJumpHeld) // Pressing longer make the jump feel lighter
        {
            multiplier = lowJumpGravityMultiplier;
        } 
        else
        {
            multiplier = baseMultiplier;
        }

        RBCharacter.AddForce(Vector3.down * multiplier * baseGravity, ForceMode.Acceleration);

    }


    bool IsGrounded()
    {
        // Check ground layer from the feet of the character
            if (groundCheck == null) return true;
            return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer) || Physics.CheckSphere(groundCheck.position, groundCheckRadius, bodyLayer);
    }
    
}
