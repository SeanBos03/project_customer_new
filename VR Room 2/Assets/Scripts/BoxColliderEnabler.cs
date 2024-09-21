using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderEnabler : MonoBehaviour
{
    BoxCollider theCollider;
    // Start is called before the first frame update
    void Start()
    {
        theCollider = GetComponent<BoxCollider>();
    }  

    public void EnableCollider()
    {
        theCollider.enabled = true;
    }

    public void DisableCollider()
    {
        theCollider.enabled = false;
    }
}
