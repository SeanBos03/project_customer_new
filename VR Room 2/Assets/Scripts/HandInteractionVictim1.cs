using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractionTriggerVictim1 : MonoBehaviour
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

    bool noRepeat = true;
    void Update()
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

        if (collisionTimerCoroutineCPR == null)
        {
            Debug.Log("CPR guy press");
            collisionTimerCoroutineCPR = StartCoroutine(CollisionTimerCPR());
        }
    }

    void ResetAnimation()
    {
        isAnimationTriggered = false;
        playerAnimator.SetInteger("State01", 0);
        int theState = playerAnimator.GetInteger("State01");
        Debug.Log("State01 reset: " + theState);
        Debug.Log("Hands are apart or out of proximity, animation reset.");

        if (collisionTimerCoroutineCPR != null)
        {
            Debug.Log("stop timer cpr");
            StopCoroutine(collisionTimerCoroutineCPR);
            collisionTimerCoroutineCPR = null;  // Reset coroutine reference
            theScript.AdvanceStage();
        }
    }

    private IEnumerator CollisionTimerCPR()
    {
        yield return new WaitForSeconds(8);

        if (noRepeat)
        {
            noRepeat = false;
            Debug.Log("CPR done");
            theScript.theStage = 5;
            theScript.ExectueStage();
            Destroy(this);
        }
        
    }
}
