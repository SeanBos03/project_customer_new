using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractionTrigger2 : MonoBehaviour
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

    Coroutine collisionTimerCoroutineBandage = null;

    void Update()
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

        //theRig.transform.position
        float handDistance = Vector3.Distance(leftHand.position, rightHand.position);
        float proximityToOther = Vector3.Distance(leftHand.transform.position, otherCharacter.transform.position);

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

        if (collisionTimerCoroutineBandage == null)
        {
            Debug.Log("Bandage animation play");
            collisionTimerCoroutineBandage = StartCoroutine(CollisionTimerBandage());
        }
    }

    void ResetAnimation()
    {
        isAnimationTriggered = false;
        playerAnimator.SetInteger("State01", 0);
        int theState = playerAnimator.GetInteger("State01");
        Debug.Log("State01 reset: " + theState);
        Debug.Log("Hands are apart or out of proximity, animation reset.");

        if (collisionTimerCoroutineBandage != null)
        {
            Debug.Log("stop timer bandage");
            StopCoroutine(collisionTimerCoroutineBandage);
            collisionTimerCoroutineBandage = null;  // Reset coroutine reference
            theScript.AdvanceStage();
        }
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
