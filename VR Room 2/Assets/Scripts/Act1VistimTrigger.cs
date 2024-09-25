using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act1VistimTrigger : MonoBehaviour
{
    [SerializeField] Act1Script theScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            theScript.AdvanceStage();
        }
    }
}
