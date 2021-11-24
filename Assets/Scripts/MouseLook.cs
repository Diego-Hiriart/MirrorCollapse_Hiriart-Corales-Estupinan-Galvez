using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 300f;

    // Public property to change sensitivity value in other scripts
    public float MouseSensitivity
    {
        get => mouseSensitivity;
        set => mouseSensitivity = value;
    }
    
    [SerializeField] Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        // Locks the cursor to the center of the screen when playing
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleCamera();
    }

    // Allows the player to move the camera according to the mouse axis movement
    void HandleCamera()
    {
        // Checks the mouse input of each axis
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        // Clamps the camera rotation value to avoid rotating the camera
        // in the Y axis to unnatural values
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Smoothly rotates the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
