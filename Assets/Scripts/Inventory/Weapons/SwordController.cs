using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponController
{

    [SerializeField] SwordModel swordModel;
    // Start is called before the first frame update
    void Start()
    {
        Setup(swordModel);
    }

}
