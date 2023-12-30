using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompanyStats;

public class SoundVetalaManager : MonoBehaviour
{
    public enum Sonidos
    {
        VETALA_GOLPE,
        VETALA_FLAPPING,
        MUERTE_VETALA,
        TOTAL_SONIDOS
    }

    [SerializeField] private Animator animator;

    private float cur_health;
    private bool alreadyDead = false;
    private bool damage, isIdle;

    public AudioSource[] audios;
    string[] nombreSonidos = { "vetala_golpe", "vetala_flapping", "muerte_vetala" };

    void Start()
    {
        audios = GetComponents<AudioSource>();

        for (int i = (int)Sonidos.VETALA_GOLPE; i < (int)Sonidos.TOTAL_SONIDOS; i++)
        {
            audios[i].clip = Resources.Load<AudioClip>(nombreSonidos[i]);
        }
    }

    void Update()
    {
        EnemyController enemy = gameObject.GetComponent<EnemyController>();
        VetalaController vetala = gameObject.GetComponent<VetalaController>();
        damage = enemy.isHit;
        isIdle = vetala.isIdle;
        cur_health = vetala.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE);

        if (Time.timeScale == 1f)
        {
            if (isIdle)
            {
                audios[(int)Sonidos.VETALA_FLAPPING].Play();
            }
            else
            {
                audios[(int)Sonidos.VETALA_FLAPPING].Pause();
            }

            if (damage)
            {
                audios[(int)Sonidos.VETALA_GOLPE].Play();
            }

            if (cur_health <= 0 && !alreadyDead)
            {
                alreadyDead = true;
                audios[(int)Sonidos.MUERTE_VETALA].Play();
            }
        }
        else
        {
            audios[(int)Sonidos.VETALA_FLAPPING].Pause();
            audios[(int)Sonidos.VETALA_GOLPE].Pause();
            audios[(int)Sonidos.MUERTE_VETALA].Pause();
        }
    }
}
