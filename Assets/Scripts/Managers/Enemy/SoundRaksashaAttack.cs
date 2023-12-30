using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRaksashaAttack : MonoBehaviour
{
    [SerializeField] private AudioSource aplastar;
    [SerializeField] private AudioSource zarpazo;
    public void AplastarPlay()
    {
        aplastar.Play();
    }

    public void ZarpazoPlay()
    {
        zarpazo.Play();
    }
}
