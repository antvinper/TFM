using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RaksashaAttackController : MonoBehaviour
{
    private RaksashaController raksashaController;
    
    private float timeBetweenApplyDamage = 2.1f;
    private bool canMakeDamage = true;

    private void Start()
    {
        raksashaController = GetComponentInParent<RaksashaController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canMakeDamage)
        {
            raksashaController.ApplySkill(other.GetComponentInChildren<PlayerController>());
            WaitBetweenMakeDamage();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canMakeDamage)
        {
            raksashaController.ApplySkill(other.GetComponentInChildren<PlayerController>());
            WaitBetweenMakeDamage();
        }
    }

    private async Task WaitBetweenMakeDamage()
    {
        canMakeDamage = false;
        await new WaitForSeconds(timeBetweenApplyDamage);
        canMakeDamage = true;
    }
}
