using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTouch2 : MonoBehaviour
{
    [SerializeField] Act1Script theScript;
    public bool shouldStart;
    void OnCollisionEnter(Collision other)
    {
        if (theScript.theStage != 9)
        {
            return;
        }

        // Check if the collider belongs to the player by checking its tag
        if (other.gameObject.CompareTag("IncorrectMedItem"))
        {
            Debug.Log("wrong item");
            Destroy(other.gameObject);
            theScript.ExectueStage();
        }

        if (other.gameObject.CompareTag("CorrectBandage"))
        {
            Debug.Log("correct item");
            Destroy(other.gameObject);
            theScript.theStage++;
            theScript.ExectueStage();
        }
    }


}
