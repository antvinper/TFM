using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyParticules : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AudioSource buff;

    public void EffectAnimation()
    {
        particle.Play();
        buff.Play();
    }
}
