using UnityEngine;
using System.Collections;

public class VRTouch : MonoBehaviour
{
    float waitTime = 6f;
    Coroutine collisionTimerCoroutine = null;

    [SerializeField] Act1Script theScript;
    public bool shouldStart;

    void OnTriggerEnter(Collider other)
    {
        if (shouldStart == false)
        {
            return;
        }

        if (theScript.theStage == 4)
        {
            // Check if the collided object matches the specific tag
            if (other.CompareTag("DaHand"))
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
        if (shouldStart == false)
        {
            return;
        }

        if (theScript.theStage == 4)
        {
            if (other.CompareTag("DaHand"))
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
        Debug.Log("pulse check done");
        theScript.ExectueStage();
    }
}