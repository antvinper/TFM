using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompArea : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] SkillDefinition skill;
    public SardulaBehavior sardula;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController target = other.GetComponentInChildren<PlayerController>();
            skill.ProcessSkill(sardula, target);
            gameObject.SetActive(false);
        }
    }
}
