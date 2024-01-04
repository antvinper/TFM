using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashArea : MonoBehaviour
{
    [SerializeField] SkillDefinition skill;
    public SardulaBehavior sardula;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController target = other.GetComponent<PlayerController>();
            skill.ProcessSkill(sardula, target);
        }
    }
}
