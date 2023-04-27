using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ComboDefinition : ScriptableObject, IComboModel
{
    public WeaponModel weaponModel;
    public float[] damageMultiplier;
    public ButtonsXbox[] buttons;

    public WeaponModel WeaponModel => weaponModel;

    public float[] DamageMultiplier => damageMultiplier;

    public ButtonsXbox[] Buttons => buttons;
}
