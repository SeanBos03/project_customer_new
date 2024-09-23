using System.Collections;
using UnityEngine;

public class UFOTractorBeam : MonoBehaviour
{
    public Transform player;         // Reference to the player object
    public float moveSpeed = 5f;     // Speed at which the UFO moves towards the player
    public float dragSpeed = 3f;     // Speed at which the player is dragged towards the UFO
    public float stopDistance = 1f;  // Distance at which the UFO stops when it gets near the player

    private bool isMovingToPlayer = false;
    private bool isDraggingPlayer = false;

    void Update()
    {
        // On key press (for example, 'E' key), start the movement
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isMovingToPlayer && !isDraggingPlayer)
            {
                StartCoroutine(MoveToPlayer());
            }
        }
    }

    IEnumerator MoveToPlayer()
    {
        isMovingToPlayer = true;

        // Move the UFO towards the player until it's within stopDistance
        while (Vector3.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Start dragging the player after the UFO reaches the player
        isDraggingPlayer = true;
        StartCoroutine(DragPlayerToUFO());
    }

    IEnumerator DragPlayerToUFO()
    {
        while (isDraggingPlayer)
        {
            player.position = Vector3.MoveTowards(player.position, transform.position, dragSpeed * Time.deltaTime);

            // If the player is close enough to the UFO, stop dragging
            if (Vector3.Distance(player.position, transform.position) < 0.1f)
            {
                isDraggingPlayer = false;
                break;
            }

            yield return null;
        }

        // Reset flags when the player has been dragged to the UFO
        isMovingToPlayer = false;
    }
}
