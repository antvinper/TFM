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
        isActive = startsActive;
    }

    public bool StartCombo(ButtonsXbox buttonPressed)
    {
        bool activated = false;
        if (buttonPressed == buttons[weaponController.ActualIndex])
        {
            Debug.Log("#COMBO# Combo Started: " + this.name + " button: " + buttons[weaponController.ActualIndex]);
            activated = true;
            //++weaponController.ActualIndex;
        }

        return activated;
    }

    internal void SetActive(bool isActive)
    {
        this.isActive = isActive;
    }

    public bool ContinueCombo(ButtonsXbox buttonPressed, List<ButtonsXbox> actualActionStack)
    {
        bool comboContinued = false;

        bool isThisCombo = true;
        for(int i = 0; i < actualActionStack.Count; ++i)
        {
            if(actualActionStack[i] != buttons[i])
            {
                isThisCombo = false;
                break;
            }
        }

        if(isThisCombo && buttonPressed == buttons[weaponController.ActualIndex])
        {
            if(weaponController.ActualIndex == buttons.Length-1)
            {
                weaponController.ContinueAnimationCombo();
                comboFinished = true;
                weaponController.FinishCombo();
                Debug.Log("#COMBO# Combo Finished: " + this.name + " button: " + buttons[weaponController.ActualIndex]);
            }
            else
            {
                comboContinued = true;
                Debug.Log("#COMBO# Combo Continued: " + this.name + " actualIndex = " + weaponController.ActualIndex + " button: " + buttons[weaponController.ActualIndex]);
                //++weaponController.ActualIndex;
            }

            weaponController.DoingCombo = true;
        }

        return comboContinued;
    }

    public int GetComboLength()
    {
        return buttons.Length;
    }
}
