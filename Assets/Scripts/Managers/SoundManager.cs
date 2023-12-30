using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public enum Sonidos
    {
        RUN,
        TOTAL_SONIDOS
    }
    public AudioSource[] audios;

    string[] nombreSonidos = { "run" };

    void Start()
    {
        audios = GetComponents<AudioSource>();

        for (int i = (int)Sonidos.RUN; i < (int)Sonidos.TOTAL_SONIDOS; i++)
        {
            audios[i].clip = Resources.Load<AudioClip>(nombreSonidos[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1f)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (!audios[(int)Sonidos.RUN].isPlaying)
                {
                    audios[(int)Sonidos.RUN].Play();
                }
            }
            else
            {
                if (audios[(int)Sonidos.RUN].isPlaying)
                {
                    audios[(int)Sonidos.RUN].Pause();
                }
            }
        }
        else
        {
            audios[(int)Sonidos.RUN].Pause();
        }
    }
}
