using UnityEngine;

public class VRTouch : MonoBehaviour
{

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("detect somethig");
        Debug.Log(other.tag);
        // Check if the collided object matches the specific tag
        if (other.CompareTag("GameController"))
        {
            Debug.Log("Victim touched");
        }
    }

    void OnCollision(Collider other)
    {
        Debug.Log("detect somethig collision");
        Debug.Log(other.tag);
        // Check if the collided object matches the specific tag
        if (other.CompareTag("GameController"))
        {
            Debug.Log("Victim touched collision");
        }
    }
}