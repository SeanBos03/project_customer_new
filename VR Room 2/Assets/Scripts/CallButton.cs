using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CallButton : MonoBehaviour
{
    [SerializeField] string theNumber;
    [SerializeField] string theSecondNumber;
    
    [SerializeField] GameObject thePhone;
    [SerializeField] Act1Script theScript;

    PhoneDial phoneDialScript;
    XRSimpleInteractable theInteractable;

    [SerializeField] AudioClip phoneDialSound;
    [SerializeField] GameObject audioSourceObject;
    AudioSource playerAudioSource;

    public bool hasCalled;

    void Start()
    {
        phoneDialScript = thePhone.GetComponent<PhoneDial>();
        theInteractable = GetComponent<XRSimpleInteractable>();
        theInteractable.firstSelectEntered.AddListener(CallNumber); //subcribe first select entered event
        playerAudioSource = audioSourceObject.GetComponent<AudioSource>();
    }

    void CallNumber(SelectEnterEventArgs args)
    {
        if (phoneDialScript.TheNumber == theNumber || 
            phoneDialScript.TheNumber == theSecondNumber)
        {
            Debug.Log("Call success: " + theScript.theStage);
            playerAudioSource.clip = phoneDialSound;
            playerAudioSource.Play();


            if (theScript.theStage == 6)
            {
                theScript.ExectueStage();
                hasCalled = true;
            }
        }

        else
        {
            playerAudioSource.clip = phoneDialSound;
            playerAudioSource.Play();
            Debug.Log("Call failed: " + theScript.theStage);

            if (theScript.theStage == 6)
            {
                theScript.AdvanceStage();
            }
        }
    }
}
