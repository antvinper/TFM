using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BeamProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] SkillDefinition skill;
    public SardulaBehavior sardula;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController target = other.GetComponent<PlayerController>();
            skill.ProcessSkill(sardula, target);
            gameObject.SetActive(false);
        }
    }
}
