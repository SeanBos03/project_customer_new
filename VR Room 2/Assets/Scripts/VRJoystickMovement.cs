using UnityEngine;
using UnityEngine.InputSystem;

public class XRJoystickMovement : MonoBehaviour
{
    public InputActionProperty leftJoystick;   // Left joystick input for movement
    public InputActionProperty rightJoystick;  // Right joystick input for turning
    public InputActionProperty sprintAction;   // Input action for sprinting

    public float normalMoveSpeed = 2.0f;       // Normal movement speed
    public float sprintMoveSpeed = 4.0f;       // Sprint movement speed
    public float turnSpeed = 60f;              // Turn speed for left/right rotation

    public Transform xrCamera;                 // Reference to the VR camera

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (xrCamera == null)
        {
            xrCamera = Camera.main.transform;
        }

        if (characterController == null)
        {
            Debug.LogError("CharacterController component is missing on the XR Rig.");
        }
    }

    private void Update()
    {
        // Read joystick input for movement
        Vector2 leftInput = leftJoystick.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(leftInput.x, 0, leftInput.y);

        // Convert movement direction relative to camera orientation, ignore y-axis to prevent floating
        Vector3 movement = xrCamera.TransformDirection(moveDirection);
        movement.y = 0;  // Ensures no vertical movement is applied

        // Determine movement speed based on sprint input
        float moveSpeed = sprintAction.action.IsPressed() ? sprintMoveSpeed : normalMoveSpeed;

        // Apply movement to the CharacterController
        characterController.Move(movement * moveSpeed * Time.deltaTime);

        // Read right joystick for left/right turning
        Vector2 rightInput = rightJoystick.action.ReadValue<Vector2>();

        // Apply horizontal rotation (yaw) to the XR Rig
        float turnAmount = rightInput.x * turnSpeed * Time.deltaTime;
        transform.Rotate(0, turnAmount, 0);

        // Apply gravity manually to keep player grounded
        Vector3 gravity = Vector3.down * 9.81f * Time.deltaTime; // Adjust gravity value as needed
        characterController.Move(gravity);
    }
}