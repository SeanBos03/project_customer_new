using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class VRAudioSource : MonoBehaviour
{
    public AudioMixerGroup vrHeadsetOutput;
    public AudioMixerGroup speakerOutput;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SwitchToHeadset()
    {
        audioSource.outputAudioMixerGroup = vrHeadsetOutput;
    }

    public void SwitchToSpeakers()
    {
        audioSource.outputAudioMixerGroup = speakerOutput;
    }
}
