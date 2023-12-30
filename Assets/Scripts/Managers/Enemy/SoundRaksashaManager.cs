using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanyStats;

public class SoundRaksashaManager : MonoBehaviour
{
    public enum Sonidos
    {
        RAKSASHA_GOLPE,
        MUERTE_RAKSASHA,
        TOTAL_SONIDOS
    }

    [SerializeField] private Animator animator;

    private float cur_health;
    private bool alreadyDead = false;
    private bool damage;

    public AudioSource[] audios;
    string[] nombreSonidos = { "raksasha_golpe", "muerte_raksasha" };

    void Start()
    {
        audios = GetComponents<AudioSource>();

        for (int i = (int)Sonidos.RAKSASHA_GOLPE; i < (int)Sonidos.TOTAL_SONIDOS; i++)
        {
            audios[i].clip = Resources.Load<AudioClip>(nombreSonidos[i]);
        }
    }

    void Update()
    {
        EnemyController enemy = gameObject.GetComponent<EnemyController>();
        RaksashaController raksasha = gameObject.GetComponent<RaksashaController>();
        damage = enemy.isHit;

        cur_health = raksasha.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE);

        if (Time.timeScale == 1f)
        {
            if (damage)
            {
                audios[(int)Sonidos.RAKSASHA_GOLPE].Play();
            }
        }

        if (Time.timeScale == 1f)
        {
            if (cur_health <= 0 && !alreadyDead)
            {
                alreadyDead = true;
                audios[(int)Sonidos.MUERTE_RAKSASHA].Play();
            }
        }
        else
        {
            audios[(int)Sonidos.RAKSASHA_GOLPE].Pause();
            audios[(int)Sonidos.MUERTE_RAKSASHA].Pause();
        }
    }
}
