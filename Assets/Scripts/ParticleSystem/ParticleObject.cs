using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObject : MonoBehaviour
{
    private ParticleSystem _particle;
    private ParticleSystem.EmissionModule _emmision;
    public void Setup()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
        _emmision = _particle.emission;
        _particle.Stop();
    }

    public void Play()
    {
        _particle.Play();
    }

    public void Stop()
    {
        _particle.Stop();
    }
}
