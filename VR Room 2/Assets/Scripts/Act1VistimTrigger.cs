using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act1VistimTrigger : MonoBehaviour
{
    [SerializeField] Act1Script theScript;
    SphereCollider sphereCollider;
    VRTouch vrTouch2Script;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        vrTouch2Script = GetComponent<VRTouch>();
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("ACttivate");
            Debug.Log(theScript.theStage);
            if (theScript.theStage == 3)
            {
                sphereCollider.enabled = false;
                vrTouch2Script.shouldStart = true;
                theScript.ExectueStage();
                Debug.Log("near the victim");
            }
            
        }
    }
}
