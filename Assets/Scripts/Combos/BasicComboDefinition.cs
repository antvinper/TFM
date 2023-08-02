using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicCombo", menuName = "TFM/Combos/Basic Combo Definition")]
public class BasicComboDefinition : ComboDefinition
{
    WeaponController weaponController;

    public void SetUp(WeaponController weaponController)
    {
        this.weaponController = weaponController;
    }

    public bool StartCombo(ButtonsXbox buttonPressed)
    {
        bool activated = false;
        if (buttonPressed == buttons[weaponController.actualIndex])
        {
            Debug.Log("#COMBO# Combo Started: " + this.name + " button: " + buttons[weaponController.actualIndex]);
            activated = true;
            ++weaponController.actualIndex;
        }

        return activated;
    }

    internal void SetActive(bool isActive)
    {
        this.isActive = isActive;
    }

    public bool ContinueCombo(ButtonsXbox buttonPressed)
    {
        bool comboContinued = false;

        if(buttonPressed == buttons[weaponController.actualIndex])
        {
            if(weaponController.actualIndex == buttons.Length-1)
            {
                comboFinished = true;
                weaponController.FinishCombo();
                Debug.Log("#COMBO# Combo Finished: " + this.name + " button: " + buttons[weaponController.actualIndex]);
            }
            else
            {
                comboContinued = true;
                Debug.Log("#COMBO# Combo Continued: " + this.name + " actualIndex = " + weaponController.actualIndex + " button: " + buttons[weaponController.actualIndex]);
                ++weaponController.actualIndex;
            }

            weaponController.doingCombo = true;
        }

        return comboContinued;
    }
}
