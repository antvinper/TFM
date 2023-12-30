using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterAreaZone : MonoBehaviour
{
    public bool player = false;
    public bool boss = false;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")){
            player = true;
        }

        else if(other.CompareTag("Sardula")){
            boss = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")){
            player = false;
        }
        else if (other.CompareTag("Sardula")){
            boss = false;
        }
    }
}
