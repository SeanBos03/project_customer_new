using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public WaypointMarker waypointMarker;

    void Start()
    {
        if (waypoints.Length > 0)
        {
            waypointMarker.SetWaypointTarget(waypoints[currentWaypointIndex]);
        }
    }

    void Update()
    {
        float distanceToWaypoint = Vector3.Distance(waypoints[currentWaypointIndex].position, waypointMarker.player.position);
        if (distanceToWaypoint < 1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex < waypoints.Length)
            {
                waypointMarker.SetWaypointTarget(waypoints[currentWaypointIndex]);
            }
        }
    }
}
