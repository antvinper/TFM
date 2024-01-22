using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaksashaPursueController : MonoBehaviour
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
            raksashaController.OnPlayerInPursueRange(true, other.GetComponentInChildren<PlayerController>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            raksashaController.OnPlayerInPursueRange(false, other.GetComponentInChildren<PlayerController>());
        }
    }
}
