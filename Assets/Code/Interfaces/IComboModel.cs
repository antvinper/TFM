using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComboModel
{
    //public WeaponModel WeaponModel { get; }
    public float[] damageMultiplier { get; }
    public ButtonsXbox[] buttons { get; }
    //public bool activated { get; set; }
    public bool comboFinished { get; set; }
}
