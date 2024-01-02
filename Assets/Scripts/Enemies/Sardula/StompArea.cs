using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompArea : MonoBehaviour
{
    // Update is called once per frame

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ouch");
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
