using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ComboDefinition : ScriptableObject, IComboModel
{
    //public WeaponModel weaponModel;
    public float[] DamageMultiplier;
    public ButtonsXbox[] Buttons;
    //public bool Activated = false;
    protected bool ComboFinished = false;

    //protected bool doingCombo = false;
    //public WeaponModel WeaponModel => weaponModel;

    public float[] damageMultiplier => DamageMultiplier;
    public ButtonsXbox[] buttons => Buttons;
    //public bool activated { get => Activated; set => Activated = value; }
    public bool comboFinished { get => ComboFinished; set => ComboFinished = value; }

    /*private void Awake()
    {
        activated = false;
    }*/
}
