using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractionTrigger : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;
    public float triggerDistance = 0.2f;
    public Animator playerAnimator;

    private bool isAnimationTriggered = false;

    void Update()
    {
        float distance = Vector3.Distance(leftHand.position, rightHand.position);

        if (distance <= triggerDistance && !isAnimationTriggered)
        {
            TriggerAnimation();
        }
        else if (distance > triggerDistance && isAnimationTriggered)
        {
            ResetAnimation();
        }
    }

    void TriggerAnimation()
    {
        isAnimationTriggered = true;
        playerAnimator.SetTrigger("HandsTogether");
    }

    void ResetAnimation()
    {
        isAnimationTriggered = false;
        playerAnimator.ResetTrigger("HandsTogether");
    }
}
