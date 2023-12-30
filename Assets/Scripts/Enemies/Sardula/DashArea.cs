using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashArea : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ouch");
        }
    }
}
