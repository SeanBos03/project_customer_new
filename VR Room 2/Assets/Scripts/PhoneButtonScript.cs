using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PhoneButtonScript : MonoBehaviour
{
    [SerializeField] char theNumber;
    [SerializeField] GameObject thePhone;

    PhoneDial phoneDialScript;
    XRSimpleInteractable theInteractable;

    void Start()
    {
        phoneDialScript = thePhone.GetComponent<PhoneDial>();
        theInteractable = GetComponent<XRSimpleInteractable>();
        theInteractable.firstSelectEntered.AddListener(AddNumber); //subcribe first select entered event
    }

    void AddNumber(SelectEnterEventArgs args)
    {
        phoneDialScript.AddNumber(theNumber);
    }
}
