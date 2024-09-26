using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class Act1Script : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] GameObject audioSourceObject;
    [SerializeField] GameObject theVictim;
    AudioClip theAudioClip;
    AudioSource playerAudioSource;

    public int theStage;
    bool repeatCheck;

    // Start is called before the first frame update
    void Start()
    {
        // Check if XR is initialized and running
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            // Print when the VR environment is loaded
            Debug.Log("VR is loaded and ready!");
            playerAudioSource = audioSourceObject.GetComponent<AudioSource>();
            theStage++;
            ExectueStage();
        }
        else
        {
            // Start a coroutine to wait for XR initialization
            StartCoroutine(CheckVRLoaded());
        }
        
    }

    // Coroutine to wait until XR is initialized
    private System.Collections.IEnumerator CheckVRLoaded()
    {
        // Wait until the XR system is fully initialized
        while (!XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            yield return null; // Wait for the next frame
        }

        // Once XR is initialized, print the message
        Debug.Log("VR is now loaded and ready!");
    }

    private void Update()
    {
        if (repeatCheck)
        {
            RepeatCheckStage();
        }
    }

    void RepeatCheckStage()
    {
        switch (theStage)
        {
            case 1:
                if (!playerAudioSource.isPlaying)
                {
                    theStage = 2;
                    repeatCheck = false;
                    ExectueStage();
                }
                break;
        }
    }

    public void AdvanceStage()
    {
        switch (theStage)
        {
            case 0:
                theStage++;
                ExectueStage();
                break;
            case 1:
                DistractPlay();
                break;
            case 2:
                theStage++;
                ExectueStage();
                break;
            case 3:
                DistractPlay();
                break;
            case 4:
                DistractPlay();
                break;
        }
    }

    void DistractPlay()
    {
        if (!playerAudioSource.isPlaying)
        {
            playerAudioSource.clip = audioClips[2];
            playerAudioSource.Play();
            Debug.Log("Distract play");
        }
    }

    public void ExectueStage()
    {
        switch (theStage)
        {
            case 1:
                playerAudioSource.clip = audioClips[0];
                playerAudioSource.Play();
                Debug.Log("Intro sound play");
                repeatCheck = true;
                    break;
            case 2:
                playerAudioSource.clip = audioClips[1];
                playerAudioSource.Play();
                Debug.Log("Intro sound 2 play");
                theVictim.SetActive(true);
                theStage++;
                break;
            case 3:
                playerAudioSource.clip = audioClips[3];
                playerAudioSource.Play();
                Debug.Log("Check breathing play");
                theStage++;
                break;
            case 4:
                playerAudioSource.clip = audioClips[4];
                playerAudioSource.Play();
                Debug.Log("victim breathing play");
                theStage++;
                break;



        }
    }

}
