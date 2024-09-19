using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CallButton : MonoBehaviour
{
    [SerializeField] string theNumber;
    [SerializeField] string theSecondNumber;
    [SerializeField] GameObject thePhone;

    PhoneDial phoneDialScript;
    XRSimpleInteractable theInteractable;

    void Start()
    {
        phoneDialScript = thePhone.GetComponent<PhoneDial>();
        theInteractable = GetComponent<XRSimpleInteractable>();
        theInteractable.firstSelectEntered.AddListener(CallNumber); //subcribe first select entered event
    }

    void CallNumber(SelectEnterEventArgs args)
    {
        if (phoneDialScript.TheNumber == theNumber || 
            phoneDialScript.TheNumber == theSecondNumber)
        {
            Debug.Log("Call success");
        }

        else
        {
            Debug.Log("Call failed");
        }
    }
}
