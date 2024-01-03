using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompArea : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] SkillDefinition skill;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController target = other.GetComponent<PlayerController>();

            Debug.Log(target.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));
            skill.ProcessSkill(target);
            Debug.Log(target.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));

            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
