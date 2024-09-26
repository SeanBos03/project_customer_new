using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act1Victim2Trigger : MonoBehaviour
{
    [SerializeField] Act1Script theScript;
    SphereCollider sphereCollider;
    VRTouch2 vrTouch2Script;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        vrTouch2Script = GetComponent<VRTouch2>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(theScript.theStage);
            if (theScript.theStage == 8)
            {
                sphereCollider.enabled = false;
                vrTouch2Script.shouldStart = true;
                theScript.ExectueStage();
                Debug.Log("near the victim 2");
                
            }

        }
    }
}
