using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] AudioSource stepsAudio;
    [SerializeField] AudioSource sprintAudio;

    [SerializeField] bool canJump = false;

    [SerializeField] float speed = 6f;
    float originalSpeed;
    float sprintTime;
    bool isSprinting = false;
    [SerializeField] float sprintLimitTime = 3f;
    [SerializeField] float sprintMultiplier = 1.25f;

    Vector3 originalCenter;
    float originalHeight;
    bool isCrouching = false;
    bool comingUp = false;

    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;

    [SerializeField] Transform groundCheck;
    float groundDistance = 0.4f;
    [SerializeField] LayerMask groundmask;

    Vector3 velocity;
    bool isGrounded;

    bool playStepAudio = false;
    bool playSprintAudio = false;

    int countStep = 0;
    int countSprint = 0;
    
    void Start() 
    {
        controller = GetComponent<CharacterController>();   

        // Store the original values
        originalSpeed = speed;
        transform.tag = "Player";
        originalCenter = controller.center;
        originalHeight = controller.height;

        stepsAudio.loop = true;
        sprintAudio.loop = true;
    }

    void Update()
    {
        HandleMove();
        HandleJump();
        HandleSprint();
        HandleCrouch();  
    }

    // Allows the player to move while using the WASD or Arrow keys
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

        if((x != 0 || z != 0) && (!isCrouching || !isSprinting))
        {
            playStepAudio = true;
        }
        else
        {
            stepsAudio.Stop();
            playStepAudio = false;
            countStep = 0;
        }

        if(countStep < 1 && playStepAudio)
        {
            stepsAudio.Play();
            countStep++;
        }

        if(playSprintAudio)
        {
            countStep = 0;
        }

        // Perform the movement to the right direction
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleJump()
    {
        // Comment the if statement if you don't want to allow jumping
        if(Input.GetButtonDown("Jump") && isGrounded && canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        }

        // Change the acceleration according to the gravity while falling down
        velocity.y += gravity * Time.deltaTime;

        // Perform the movement of falling down
        controller.Move(velocity * Time.deltaTime);
    }

    // Allows player to crouch while pressing the left control button
    void HandleCrouch()
    {
        // When pressing down the button, the player changes its height to
        // a lower one and lose speed
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height = 0.5f;
            controller.center = new Vector3(0f, (-0.5f/2), 0f);
            speed /= 2;
            isCrouching = true;
        }

        // When the key is no longer pressed, then the player is
        // standing up again
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            comingUp = true;
        }

        // Smoothly stand up again if the player is no longer crouching
        if(comingUp)
        {
            controller.center = originalCenter;
            controller.height += 0.05f;
            controller.height = Mathf.Clamp(controller.height, 1f, originalHeight);
            speed = originalSpeed;
        }

        // When the player completely stands up, the isCrouching is false
        if(controller.height == originalHeight)
        {
            comingUp = false;
            isCrouching = false;
        }
    }

    // Allows player to sprint for an amount of time while using the left shift key
    void HandleSprint()
    {
        // While the key is pressed and the player is grounded, add seconds to
        // the sprintTime value
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded && !isCrouching) 
        {
            sprintTime += 1 / (1 / Time.deltaTime);
        }

        // When pressing down the key, add the sprintMultiplier value to the current speed
        // and change the state of the player to isSprinting
        if (Input.GetKeyDown(KeyCode.LeftShift) && sprintTime <= sprintLimitTime && isGrounded && !isCrouching) 
        {
            speed *= sprintMultiplier;
            isSprinting = true;
            playSprintAudio = true;
            playStepAudio = false;
        }

        // When the key is no longer pressed or the sprintTime reached the limit, change the
        // speed to the original value and change isSprinting to false
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching || sprintTime > sprintLimitTime && isGrounded) 
        {
            speed = originalSpeed;
            isSprinting = false;
            playSprintAudio = false;
            sprintAudio.Stop();
            countSprint = 0;
        }

        if(countSprint < 1 && playSprintAudio)
        {
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                sprintAudio.Play();
                countSprint++;
                stepsAudio.Stop();
                Debug.Log("c");
            }
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
