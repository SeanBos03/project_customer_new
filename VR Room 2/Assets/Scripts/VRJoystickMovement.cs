using UnityEngine;
using UnityEngine.InputSystem;

public class XRJoystickMovement : MonoBehaviour
{
    public InputActionProperty leftJoystick; //left joystick input for movement
    public InputActionProperty rightJoystick; //right joystick input for turning
    public InputActionProperty sprintAction; //sprint input

    public float normalMoveSpeed = 2.0f;  //walk speed
    public float sprintMoveSpeed = 4.0f; //sprint speed
    public float turnSpeed = 60f; //turn speed
    public float gravityValue = -9.81f; //gravity value
    public float joystickDeadzone = 0.1f; //deadzone for joystick input

    private CharacterController characterController;
    private AudioSource playerAudioSource;
    private Vector3 playerVelocity;

    [SerializeField] GameObject audioSourceObject;
    [SerializeField] AudioClip theAudioClip;

    // Reference to the VR camera
    public Transform cameraTransform;

    private void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        if (playerAudioSource == null)
        {
            Debug.LogError("AudioSource missing");
        }

        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController missing");
        }

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleTurning();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        //read joystick input for movement
        Vector2 leftInput = leftJoystick.action.ReadValue<Vector2>();
        if (leftInput.magnitude > joystickDeadzone)
        {
            // Get the camera's forward and right vectors for movement direction
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            // Ensure movement direction is only on the XZ plane (no vertical movement)
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Calculate movement direction relative to the camera's orientation
            Vector3 moveDirection = (cameraForward * leftInput.y + cameraRight * leftInput.x);

            // Determine movement speed based on sprint input
            float moveSpeed = sprintAction.action.IsPressed() ? sprintMoveSpeed : normalMoveSpeed;

            // Apply movement to the CharacterController
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            // Play walking/running sound if not already playing
            if (!playerAudioSource.isPlaying)
            {
                playerAudioSource.clip = theAudioClip;
                playerAudioSource.Play();
            }
        }
        else
        {
            playerAudioSource.Stop(); // Stop audio when there's no input
        }
    }

    private void HandleTurning()
    {
        // Read right joystick for left/right turning
        Vector2 rightInput = rightJoystick.action.ReadValue<Vector2>();

        // Ensure turning occurs only when joystick input exceeds the deadzone
        if (Mathf.Abs(rightInput.x) > joystickDeadzone)
        {
            // Apply horizontal rotation (yaw) only to the player's vertical axis (Y)
            float turnAmount = rightInput.x * turnSpeed * Time.deltaTime;

            // Rotate the player around the Y-axis (up) in place, maintaining position
            transform.Rotate(Vector3.up, turnAmount);
        }
    }

    // Custom gravity handling for player
    private void ApplyGravity()
    {
        // Apply gravity if the player is not grounded
        if (!characterController.isGrounded)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }
        else
        {
            playerVelocity.y = 0f;  // Reset vertical velocity when grounded
        }

        // Apply gravity to the CharacterController
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}