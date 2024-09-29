using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class Act1Script : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    
    [SerializeField] GameObject theVictim;
    [SerializeField] GameObject theVictim2;

    [SerializeField] GameObject theUFO1;
    [SerializeField] GameObject theWaypoint1;

    [SerializeField] GameObject theUFO2;
    [SerializeField] GameObject theWaypoint2;

    [SerializeField] GameObject audioSourceObject;
    AudioClip theAudioClip;
    AudioSource playerAudioSource;

    [SerializeField] CallButton theCallButton;

    public int theStage;
    bool repeatCheck;

    private void Start()
    {
        playerAudioSource = audioSourceObject.GetComponent<AudioSource>();
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
            case 6:
                if (!playerAudioSource.isPlaying)
                {
                    theStage++;
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
            case 5:
                DistractPlay();
                    break;
            case 6:
                MistakePlay();
                break;
            case 11:
                MistakePlay();
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

    void MistakePlay()
    {
        int theNumber = Random.Range(8, 12);
        playerAudioSource.clip = audioClips[theNumber];
        playerAudioSource.Play();
        Debug.Log("mistake play");
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
                theWaypoint1.SetActive(true);
                theStage++;
                break;
            case 3:
                playerAudioSource.clip = audioClips[3];
                playerAudioSource.Play();
                theWaypoint1.SetActive(false);
                Debug.Log("Check breathing play");
                theStage++;
                break;
            case 4:
                playerAudioSource.clip = audioClips[4];
                playerAudioSource.Play();
                theStage++;
                Debug.Log("cpr start play");
                break;
            case 5:
                playerAudioSource.clip = audioClips[5];
                playerAudioSource.Play();
                theStage++;
                Debug.Log("phone call play");
                break;
            case 6:
                playerAudioSource.clip = audioClips[13];
                playerAudioSource.Play();
                theUFO1.SetActive(true);
                repeatCheck = true;
                Debug.Log("Phone suceed");
                break;
            case 7:

                if (theCallButton.hasCalled == false)
                {
                    theStage--;
                    ExectueStage();
                    break;
                }

                else
                {
                    playerAudioSource.clip = audioClips[6];
                    playerAudioSource.Play();
                    theWaypoint2.SetActive(true);
                    Debug.Log("next victim");
                    theStage++;
                    break;
                }
                
            case 8:
                playerAudioSource.clip = audioClips[7];
                playerAudioSource.Play();
                theWaypoint2.SetActive(false);
                Debug.Log("victim 2 check");
                theStage++;
                break;
            case 9:
                MistakePlay();
                Debug.Log("wrong item play");
                break;
            case 10:
                Debug.Log("correct item play");
                theStage++;
                break;
            case 11:
                break;
            case 12:
                playerAudioSource.clip = audioClips[14];
                playerAudioSource.Play();
                theUFO2.SetActive(true);
                theStage++;
                StartCoroutine(SwitchSceneAfterDelay());
                break;

                /*
                    case 5:
                        playerAudioSource.clip = audioClips[5];
                        playerAudioSource.Play();
                        Debug.Log("phone call play");
                        theStage++;
                        break;
                    case 7:
                        playerAudioSource.clip = audioClips[6];
                        playerAudioSource.Play();
                        Debug.Log("call succeed play");
                        theStage++;
                        break;
                    case 8:
                        playerAudioSource.clip = audioClips[7];
                        playerAudioSource.Play();
                        theStage++;
                        Debug.Log("victim 2  play");
                        break;
                    case 9:
                        int theNumber = Random.Range(8, 12);
                        playerAudioSource.clip = audioClips[theNumber];
                        playerAudioSource.Play();
                        Debug.Log("wrong item play");
                        break;
                    case 10:
                        playerAudioSource.clip = audioClips[12];
                        playerAudioSource.Play();
                        Debug.Log("correct item play");
                        break;
                */
        }
    }

    IEnumerator SwitchSceneAfterDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(10);

        // Load the scene named "End Scene"
        SceneManager.LoadScene("TheEnd");
    }
}
