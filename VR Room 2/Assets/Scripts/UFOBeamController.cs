using UnityEngine;

public class UFOBeamController : MonoBehaviour
{
    public Light ufoLight;
    public ParticleSystem beamParticles;

    void Start()
    {
        // Optionally disable the beam at the start
        ufoLight.enabled = false;
        beamParticles.Stop();
    }

    void ActivateBeam()
    {
        // Enable the light and particle system when activating the beam
        ufoLight.enabled = true;
        beamParticles.Play();
    }

    void DeactivateBeam()
    {
        // Disable the light and particle system when deactivating the beam
        ufoLight.enabled = false;
        beamParticles.Stop();
    }
}
