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

    void Update()
    {
        float handDistance = Vector3.Distance(leftHand.position, rightHand.position);
        float proximityToOther = Vector3.Distance(transform.position, otherCharacter.position);

        // Check if hands are together AND the player is within proximity of the other character
        if (handDistance <= triggerDistance && proximityToOther <= proximityDistance && !isAnimationTriggered)
        {
            TriggerAnimation();
        }
        else if ((handDistance > triggerDistance || proximityToOther > proximityDistance) && isAnimationTriggered)
        {
            ResetAnimation();
        }
    }

    void TriggerAnimation()
    {
        isAnimationTriggered = true;
        playerAnimator.SetTrigger("HandsTogether");
        Debug.Log("Hands are together and within proximity of the other character, animation triggered!");
    }

    void ResetAnimation()
    {
        isAnimationTriggered = false;
        playerAnimator.ResetTrigger("HandsTogether");
        Debug.Log("Hands are apart or out of proximity, animation reset.");
    }
}
