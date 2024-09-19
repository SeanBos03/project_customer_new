using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PhoneDial : MonoBehaviour
{
    [SerializeField] TMP_Text theText;
    string theNumber = "";

    public string TheNumber
    {
        get { return theNumber; }
        private set { }
    }

    void UpdateText()
    {
        theText.text = theNumber;
    }
    public void AddNumber(char theNumber)
    {
        if (this.theNumber == "----")
        {
            this.theNumber = "";
        }

        this.theNumber = this.theNumber + theNumber;
        UpdateText();
    }
    
    public void ClearNumber()
    {
        theNumber = "----";
        UpdateText();
    }
}
