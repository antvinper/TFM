using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaksashaAttackController : MonoBehaviour
{
    private RaksashaController raksashaController;

    private void Start()
    {
        raksashaController = GetComponentInParent<RaksashaController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            raksashaController.OnPlayerInAttackRange(true, other.GetComponentInChildren<PlayerController>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            raksashaController.OnPlayerInAttackRange(false, other.GetComponentInChildren<PlayerController>());
        }
    }
}
