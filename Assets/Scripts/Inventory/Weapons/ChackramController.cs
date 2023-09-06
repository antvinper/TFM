using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChackramController : WeaponController
{
    [SerializeField] ChackramModel chackramModel;
    // Start is called before the first frame update
    void Start()
    {
        Setup(chackramModel);
    }
}
