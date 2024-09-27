using UnityEngine;

public class VictimControl : MonoBehaviour
{
    public float moveSpeed = 0f;       
    private Vector3 moveDirection;      
    private bool isBeingDragged = false; 

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  
    }

    void Update()
    {
        // If the player is not being dragged by the UFO, allow normal movement
        if (!isBeingDragged)
        {
            MovePlayer();
        }
    }

    // Function to handle player movement
    private void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");  // Get horizontal input (A/D or Left/Right arrow keys)
        float moveZ = Input.GetAxis("Vertical");    // Get vertical input (W/S or Up/Down arrow keys)

        moveDirection = new Vector3(moveX, 0, moveZ).normalized;  // Create the movement direction

        // Apply movement to the player
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    // Public function to be called when the UFO starts dragging the player
    public void StartDragging()
    {
        isBeingDragged = true;  
        rb.isKinematic = true;  
    }

    // Public function to stop dragging the player
    public void StopDragging()
    {
        isBeingDragged = false;  
        rb.isKinematic = false;  
    }
}
