using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundIaksaManager : MonoBehaviour
{
    public enum Sonidos
    {
        IAKSA_GOLPE,
        MUERTE_IAKSA,
        TOTAL_SONIDOS
    }

    [SerializeField] private Animator animator;

    private float cur_health;
    private bool alreadyDead = false;
    private bool damage;

    public AudioSource[] audios;
    string[] nombreSonidos = { "iaksa_golpe", "muerte_iaksa" };

    void Start()
    {
        audios = GetComponents<AudioSource>();

        for (int i = (int)Sonidos.IAKSA_GOLPE; i < (int)Sonidos.TOTAL_SONIDOS; i++)
        {
            audios[i].clip = Resources.Load<AudioClip>(nombreSonidos[i]);
        }
    }

    void Update()
    {
        EnemyController enemy = gameObject.GetComponent<EnemyController>();
        IaksaController iaksa = gameObject.GetComponent<IaksaController>();
        damage = enemy.isHit;

        cur_health = iaksa.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE);

        if (Time.timeScale == 1f)
        {
            if (damage)
            {
                audios[(int)Sonidos.IAKSA_GOLPE].Play();
            }
        }

        if (Time.timeScale == 1f)
        {
            if (cur_health <= 0 && !alreadyDead)
            {
                alreadyDead = true;
                audios[(int)Sonidos.MUERTE_IAKSA].Play();
            }
        }
        else
        {
            audios[(int)Sonidos.IAKSA_GOLPE].Pause();
            audios[(int)Sonidos.MUERTE_IAKSA].Pause();
        }
    }
}
