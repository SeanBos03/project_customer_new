using System.Collections;
using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    public GameObject thePlayerObject;
    Transform player;
    public float moveSpeed = 10f;
    public float hoverHeight = 40f;
    public float dragSpeed = 3f;
    public float stopDistance = 1f;

    private VictimControl playerController;

    private bool isMovingToPlayer = false;
    private bool isHoveringAbovePlayer = false;
    private bool isDraggingPlayer = false;

    public Light ufoSpotlight;
    public ParticleSystem beamParticles;

    Rigidbody playerRb;

    void Start()
    {
        player = thePlayerObject.transform;
        playerController = player.GetComponent<VictimControl>();
        playerRb = player.GetComponent<Rigidbody>();


        if (ufoSpotlight != null)
        {
            ufoSpotlight.enabled = false;
        }
        if (beamParticles != null)
        {
            beamParticles.Stop();
        }
    }

    void Update()
    {
        if (true)
        {
            if (!isMovingToPlayer && !isHoveringAbovePlayer && !isDraggingPlayer)
            {
                StartCoroutine(MoveToPlayer());
            }
        }

        if (ufoSpotlight != null && beamParticles != null)
        {
            if (isHoveringAbovePlayer || isDraggingPlayer)
            {
                ufoSpotlight.enabled = true;
            }
            else
            {
                ufoSpotlight.enabled = false;
            }
        }
    }

    IEnumerator MoveToPlayer()
    {
        isMovingToPlayer = true;

        while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                new Vector3(player.position.x, 0, player.position.z)) > stopDistance)
        {
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isHoveringAbovePlayer = true;
        StartCoroutine(HoverAbovePlayer());
    }

    IEnumerator HoverAbovePlayer()
    {
        Vector3 hoverPosition = new Vector3(player.position.x, hoverHeight, player.position.z);

        while (Vector3.Distance(transform.position, hoverPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, hoverPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Activate particles when hovering starts
        if (beamParticles != null && !beamParticles.isPlaying)
        {
               playerRb.constraints &= ~RigidbodyConstraints.FreezePositionY; // Unfreeze Y position
           // playerRb.constraints = RigidbodyConstraints.None;
            beamParticles.Play();
        }

        isDraggingPlayer = true;
        playerController.StartDragging();
        StartCoroutine(DragPlayerToUFO());
    }

    IEnumerator DragPlayerToUFO()
    {
        while (isDraggingPlayer)
        {
            player.position = Vector3.MoveTowards(player.position, transform.position, dragSpeed * Time.deltaTime);

            if (Vector3.Distance(player.position, transform.position) < 0.1f)
            {
                isDraggingPlayer = false;
                playerController.StopDragging();
                break;
            }

            yield return null;
        }

        // Stop particles when dragging ends
        if (beamParticles != null && beamParticles.isPlaying)
        {
            beamParticles.Stop();
        }

        thePlayerObject.SetActive(false);
        //
      //  playerRb.constraints = RigidbodyConstraints.None;
        isMovingToPlayer = false;
        isHoveringAbovePlayer = false;
    }
}
