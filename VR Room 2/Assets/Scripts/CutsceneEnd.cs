using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnd : MonoBehaviour
{
    // Set the target X position in the Inspector or in code
    [SerializeField] float targetXPosition = 216.41f;
    [SerializeField] float theTime = 1.5f;
    [SerializeField] GameObject theCamera;
    [SerializeField] GameObject theXR;

    bool shouldRepeat = true;

    // Update is called once per frame
    void Update()
    {
        if (shouldRepeat)
        {
            // Check if the object's X position is equal to the target value
            if (transform.position.x <= targetXPosition + 1)
            {
                StartCoroutine(DisableAfterTime(theTime));
            }
        }
       
    }

    IEnumerator DisableAfterTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        theCamera.SetActive(false);
        theXR.SetActive(true);
        Destroy(this);
    }
}
