using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyParticules : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    
    public void EffectAnimation()
    {
        particle.Play();
    }
}
