using UnityEngine;
using System.Collections;

//script for pulse checking
public class VRTouch : MonoBehaviour
{
    float waitTime = 6f;
    Coroutine collisionTimerCoroutine = null;

    [SerializeField] Act1Script theScript;
    public bool shouldStart;


    //start pulse check with hand controller collision with body
    void OnTriggerEnter(Collider other)
    {
        if (shouldStart == false)
        {
            return;
        }

        if (theScript.theStage == 4)
        {
            if (other.CompareTag("DaHand"))
            {
                if (collisionTimerCoroutine == null)
                {
                    collisionTimerCoroutine = StartCoroutine(CollisionTimer());
                }
            }
        }
            
    }

    //fail pulse check, restart process
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
                    StopCoroutine(collisionTimerCoroutine);
                    collisionTimerCoroutine = null;  // Reset coroutine reference
                    theScript.AdvanceStage();
                }
            }
        }
            
    }

    //pulse timer
    private IEnumerator CollisionTimer()
    {
        yield return new WaitForSeconds(waitTime);

        if (theScript.theStage == 4)
        {
            Debug.Log("pulse check done");
            theScript.ExectueStage();
        }
        
    }
}