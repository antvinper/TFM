using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComboModel
{
    //public WeaponModel WeaponModel { get; }
    public float[] DamageMultiplier { get; }
    public ButtonsXbox[] Buttons { get; }
    public bool Activated { get; }
}
