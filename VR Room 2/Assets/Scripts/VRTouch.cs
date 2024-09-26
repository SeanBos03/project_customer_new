using UnityEngine;
using System.Collections;

public class VRTouch : MonoBehaviour
{
    float waitTime = 6f;
    Coroutine collisionTimerCoroutine = null;

    [SerializeField] Act1Script theScript;

    void OnTriggerEnter(Collider other)
    {
        if (theScript.theStage == 4)
        {
            // Check if the collided object matches the specific tag
            if (other.CompareTag("GameController"))
            {
                if (collisionTimerCoroutine == null)
                {
                    collisionTimerCoroutine = StartCoroutine(CollisionTimer());
                }
            }
        }
            
    }

    void OnTriggerExit(Collider other)
    {
        if (theScript.theStage == 4)
        {
            if (other.CompareTag("GameController"))
            {
                if (collisionTimerCoroutine != null)
                {
                    Debug.Log("Timer stopped");
                    StopCoroutine(collisionTimerCoroutine);
                    collisionTimerCoroutine = null;  // Reset coroutine reference
                    theScript.AdvanceStage();
                }
            }
        }
            
    }

    private IEnumerator CollisionTimer()
    {
          // Time to wait
        yield return new WaitForSeconds(waitTime);  // Wait for the specified time

        // After 5 seconds of continuous collision
        Debug.Log("Colliding with Player for 5 seconds!");
        theScript.ExectueStage();
    }
}