using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponController
{

    [SerializeField] SwordModel swordModel;
    [HideInInspector] public bool atacando = false;

    // Start is called before the first frame update
    void Start()
    {
        Setup(swordModel);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Iaksa"))
        {
            if (atacando)
            {
                Debug.Log("Estoy atacando a Iaksa");

                other.gameObject.GetComponent<IaksaController>().DestroyEnemy();

                atacando = false;
            }
        }
    }*/

}
