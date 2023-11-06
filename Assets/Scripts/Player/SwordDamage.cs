using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) //a�adir aqu�Eel tag que lleven los enemigos
        {
            //other.gameObject.GetComponent<IaksaController>().TakeDamage(damage);
        }
    }
}
