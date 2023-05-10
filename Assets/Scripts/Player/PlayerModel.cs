using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : CharacterModel
{
    [SerializeField]
    private WeaponController ActiveWeapon;

    public WeaponController activeWeapon { get => ActiveWeapon; }
}
