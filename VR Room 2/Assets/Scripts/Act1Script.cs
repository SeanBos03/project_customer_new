using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void ExectueStage()
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
                break;

        }
    }

}
