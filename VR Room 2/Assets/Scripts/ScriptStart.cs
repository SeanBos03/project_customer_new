using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptStart : MonoBehaviour
{
    [SerializeField] Act1Script theScript;
    // Start is called before the first frame update
    void Start()
    {
        theScript.theStage++;
        theScript.ExectueStage();
    }
}
