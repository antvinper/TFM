using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] SkillDefinition skill;
    public SardulaBehavior sardula;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        gameObject.transform.localScale += new Vector3(0.1f, 0, 0);
        speed += 0.5f;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController target = other.GetComponentInChildren<PlayerController>();
            skill.ProcessSkill(sardula, target);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
