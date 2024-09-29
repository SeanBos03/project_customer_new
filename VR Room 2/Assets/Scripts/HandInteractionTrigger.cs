using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractionTrigger : MonoBehaviour
{
    //cpy and bandage script

    public Transform theParent;
    public Transform theRig;
    public Transform leftHand;
    public Transform rightHand;
    public Transform otherCharacter;  // Reference to the other character's Transform
    public float triggerDistance = 0.05f;
    public float proximityDistance = 1.0f;  // Proximity distance for the other character
    public Animator playerAnimator;

    private bool isAnimationTriggered = false;
    [SerializeField] Act1Script theScript;

    Coroutine collisionTimerCoroutineCPR = null;
    Coroutine collisionTimerCoroutineBandage = null;

    void Update()
    {
        
        //Bandage
        if (gameObject.tag == "CPR guy")
        {
            if (theScript.theStage != 5)
            {
                if (collisionTimerCoroutineCPR != null)
                {
                    StopCoroutine(collisionTimerCoroutineCPR);
                    collisionTimerCoroutineCPR = null;  // Reset coroutine reference
                    
                }
                return;
            }
        }
 

        if (gameObject.tag == "Bandage guy")
        {
            if (theScript.theStage != 11)
            {
                if (collisionTimerCoroutineBandage != null)
                {
                    StopCoroutine(collisionTimerCoroutineBandage);
                    collisionTimerCoroutineBandage = null;  // Reset coroutine reference
                }
                return;
            }
        }
        //theRig.transform.position
        float handDistance = Vector3.Distance(leftHand.position, rightHand.position);
        float proximityToOther = Vector3.Distance(leftHand.transform.position, otherCharacter.transform.position);

        if (gameObject.tag == "CPR guy")
        {
            if (theScript.theStage == 5)
            {
                //  Debug.Log("Pro: " + proximityToOther + "Hand distance" + handDistance);
            }
        }

        // Check if hands are together AND the player is within proximity of the other character
        if ((handDistance <= triggerDistance) && (proximityToOther <= proximityDistance) && !isAnimationTriggered)
        {
            TriggerAnimation();
        }
        else if (((handDistance > triggerDistance) || proximityToOther > proximityDistance) && isAnimationTriggered)
        {
            ResetAnimation();
        }
    }

    void TriggerAnimation()
    {
        isAnimationTriggered = true;
        playerAnimator.SetInteger("State01", 1);
        Debug.Log("Hands are together and within proximity of the other character, animation triggered!" + "Tag: " + gameObject.tag + "Distance: " + Vector3.Distance(leftHand.transform.position, otherCharacter.transform.position));
        int theState = playerAnimator.GetInteger("State01");
        Debug.Log("State01: " + theState);
        if (gameObject.tag == "CPR guy")
        {
            if (collisionTimerCoroutineCPR == null)
            {
                Debug.Log("CPR guy press");
                collisionTimerCoroutineCPR = StartCoroutine(CollisionTimerCPR());
            }
        }

        if (gameObject.tag == "Bandage guy")
        {
            if (collisionTimerCoroutineBandage == null)
            {
                collisionTimerCoroutineBandage = StartCoroutine(CollisionTimerBandage());
            }
        }
    }

    void ResetAnimation()
    {
        isAnimationTriggered = false;
        playerAnimator.SetInteger("State01", 0);
        int theState = playerAnimator.GetInteger("State01");
        Debug.Log("State01 reset: " + theState);
        Debug.Log("Hands are apart or out of proximity, animation reset.");

        if (gameObject.tag == "CPR guy")
        {
            if (collisionTimerCoroutineCPR != null)
            {
                Debug.Log("stop timer cpy");
                StopCoroutine(collisionTimerCoroutineCPR);
                collisionTimerCoroutineCPR = null;  // Reset coroutine reference
                theScript.AdvanceStage();
            }
        }

        if (gameObject.tag == "Bandage guy")
        {
            if (collisionTimerCoroutineBandage != null)
            {
                Debug.Log("stop timer bandage");
                StopCoroutine(collisionTimerCoroutineBandage);
                collisionTimerCoroutineBandage = null;  // Reset coroutine reference
                theScript.AdvanceStage();
            }
        }
    }

    private IEnumerator CollisionTimerCPR()
    {
        yield return new WaitForSeconds(8);
        Debug.Log("CPR done");
        theScript.ExectueStage();
        Destroy(this);
    }

    private IEnumerator CollisionTimerBandage()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Bandage done");
        theScript.theStage++;
        theScript.ExectueStage();
        Destroy(this);
    }
}
