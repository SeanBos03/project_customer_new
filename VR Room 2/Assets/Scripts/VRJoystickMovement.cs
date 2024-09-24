using UnityEngine;
using Oculus.Platform; // Optional for Oculus-specific platform functionalities
using Oculus.Platform.Models; // Optional for Oculus-specific models

public class VRJoystickMovement : MonoBehaviour
{
    public float speed = 2.0f;   // Movement speed
    public Transform playerCamera;  // The VR Camera (usually the CenterEyeAnchor in OVRCameraRig)
    private CharacterController characterController;

    void Start()
    {
        // Get the CharacterController attached to the rig
        characterController = GetComponent<CharacterController>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main.transform;  // Fallback if not assigned
        }
    }

    void Update()
    {
        // Get input from the Oculus controller's primary thumbstick (left hand controller typically)
        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        // Convert joystick input into movement vector
        Vector3 moveDirection = new Vector3(primaryAxis.x, 0, primaryAxis.y);

        // Move relative to the camera's forward direction
        moveDirection = playerCamera.TransformDirection(moveDirection);
        moveDirection.y = 0; // Ensure movement is only horizontal

        // Apply movement to the CharacterController
        characterController.Move(moveDirection * speed * Time.deltaTime);
    }
}