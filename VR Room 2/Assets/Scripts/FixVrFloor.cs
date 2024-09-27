using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixVrFloor : MonoBehaviour
{
    public GameObject xrCamera;
    void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(xrCamera.transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            // Align the XR rig's Y position with the ground
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}
