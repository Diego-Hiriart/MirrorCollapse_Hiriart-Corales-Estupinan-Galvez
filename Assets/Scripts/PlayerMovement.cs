using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField] float speed = 6f;
    float originalSpeed;
    float sprintTime;
    bool isSprinting = false;
    [SerializeField] float sprintLimitTime = 3f;
    [SerializeField] float sprintMultiplier = 1.25f;

    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;

    [SerializeField] Transform groundCheck;
    float groundDistance = 0.4f;
    [SerializeField] LayerMask groundmask;

    Vector3 velocity;
    bool isGrounded;
    
    void Start() 
    {
        // Store the original speed value
        originalSpeed = speed;
    }

    void Update()
    {
        HandleMove();
        HandleJump();
        HandleSprint();    
    }

    void HandleMove()
    {
        // Checks if the player is touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get the input of each axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Perform the movement to the right direction
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleJump()
    {
        // Comment the if statement if you don't want to allow jumping
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        }

        // Change the acceleration according to the gravity while falling down
        velocity.y += gravity * Time.deltaTime;

        // Perform the movement of falling down
        controller.Move(velocity * Time.deltaTime);
    }

    // Allows player to sprint for an amount of time while using the left shift key
    void HandleSprint()
    {
        // While the key is pressed and the player is grounded, add seconds to
        // the sprintTime value
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded) 
        {
            sprintTime += 1 / (1 / Time.deltaTime);
        }

        // When pressing down the key, add the sprintMultiplier value to the current speed
        // and change the state of the player to isSprinting
        if (Input.GetKeyDown(KeyCode.LeftShift) && sprintTime <= sprintLimitTime && isGrounded) 
        {
            speed = originalSpeed * sprintMultiplier;
            isSprinting = true;
        }

        // When the key is no longer pressed or the sprintTime reached the limit, change the
        // speed to the original value and change isSprinting to false
        if (Input.GetKeyUp(KeyCode.LeftShift) || sprintTime > sprintLimitTime && isGrounded) 
        {
            speed = originalSpeed;
            isSprinting = false;
        }

        // If the player is not sprinting, then continously substract seconds to the
        // sprintTime value until it reaches 0 again
        if(!isSprinting)
        {
            sprintTime -= 1 / (1 / Time.deltaTime);
        }

        // Clamps the sprintTime value to 0 or 3
        sprintTime = Mathf.Clamp(sprintTime, 0, sprintLimitTime);
    }
}
