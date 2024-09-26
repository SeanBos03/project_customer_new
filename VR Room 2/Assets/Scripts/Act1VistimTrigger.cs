using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act1VistimTrigger : MonoBehaviour
{
    [SerializeField] Act1Script theScript;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ACttivate");
        if (other.CompareTag("Player"))
        {
            Debug.Log(theScript.theStage);
            if (theScript.theStage == 3)
            {
                theScript.ExectueStage();
                Debug.Log("near the victim");
            }
            
        }
    }
}
