using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterProjectile : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ouch");
            Destroy(gameObject);
        }
    }
}
