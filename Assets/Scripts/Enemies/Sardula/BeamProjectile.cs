using CompanyStats;
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
            PlayerController target = other.GetComponentInChildren<PlayerController>();
            skill.ProcessSkill(sardula, target);
            gameObject.SetActive(false);
        }
    }
}
