using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVetalaFlapping : MonoBehaviour
{
    [SerializeField] private AudioSource flapping;

    public void FlappingPlay()
    {
        flapping.Play();
    }

    public void FlappingPause()
    {
        flapping.Pause();
    }
}
