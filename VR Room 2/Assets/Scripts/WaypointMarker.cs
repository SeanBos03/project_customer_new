using UnityEngine;

public class WaypointMarker : MonoBehaviour
{
    public Transform player;
    public Transform waypointTarget;
    public float heightAboveGround = 2f;

    void Update()
    {
        if (waypointTarget != null)
        {
            Vector3 targetPosition = waypointTarget.position;
            targetPosition.y += heightAboveGround;
            transform.position = targetPosition;

            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0;
            transform.rotation = Quaternion.LookRotation(directionToPlayer);
        }
    }

    public void SetWaypointTarget(Transform newTarget)
    {
        waypointTarget = newTarget;
    }
}
