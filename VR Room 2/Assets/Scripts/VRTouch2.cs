using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTouch2 : MonoBehaviour
{
    [SerializeField] Act1Script theScript;
    public bool shouldStart;
    void OnTriggerEnter(Collider other)
    {
        if (shouldStart == false)
        {
            
            return;
        }

        if (theScript.theStage != 9)
        {
            return;
        }

        // Check if the collider belongs to the player by checking its tag
        if (other.CompareTag("IncorrectMedItem"))
        {
            Debug.Log("wrong item");
            Destroy(other.gameObject);
            theScript.ExectueStage();
        }

        if (other.CompareTag("CorrectBandage"))
        {
            Debug.Log("correct item");
            Destroy(other.gameObject);
            theScript.theStage++;
            theScript.ExectueStage();
        }
    }
}
