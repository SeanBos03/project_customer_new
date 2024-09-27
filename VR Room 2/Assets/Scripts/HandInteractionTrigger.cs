using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractionTrigger : MonoBehaviour
{
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

        float handDistance = Vector3.Distance(leftHand.position, rightHand.position);
        float proximityToOther = Vector3.Distance(leftHand.transform.position, otherCharacter.position);

        

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
        playerAnimator.SetTrigger("HandsTogether");
        Debug.Log("Hands are together and within proximity of the other character, animation triggered!" + "Tag: " + gameObject.tag + "Distance: " + Vector3.Distance(leftHand.transform.position, otherCharacter.transform.position));

        if (gameObject.tag == "CPR guy")
        {
            if (collisionTimerCoroutineCPR == null)
            {
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
        playerAnimator.ResetTrigger("HandsTogether");
        Debug.Log("Hands are apart or out of proximity, animation reset.");

        if (gameObject.tag == "CPR guy")
        {
            if (collisionTimerCoroutineCPR != null)
            {
                StopCoroutine(collisionTimerCoroutineCPR);
                collisionTimerCoroutineCPR = null;  // Reset coroutine reference
                theScript.AdvanceStage();
            }
        }

        if (gameObject.tag == "Bandage guy")
        {
            if (collisionTimerCoroutineCPR != null)
            {
                StopCoroutine(collisionTimerCoroutineBandage);
                collisionTimerCoroutineBandage = null;  // Reset coroutine reference
                theScript.AdvanceStage();
            }
        }
    }

    private IEnumerator CollisionTimerCPR()
    {
        yield return new WaitForSeconds(5);
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
