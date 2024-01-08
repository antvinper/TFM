using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicCombo", menuName = "TFM/Combos/Basic Combo Definition")]
public class BasicComboDefinition : ComboDefinition
{
    WeaponController weaponController;
    int actualIndex = 0;

    public void SetUp(WeaponController weaponController)
    {
        actualIndex = 0;
        this.weaponController = weaponController;
        isActive = startsActive;
    }

    public void ResetActualIndex()
    {
        actualIndex = 0;
    }

    public bool StartCombo(ButtonsXbox buttonPressed)
    {
        bool activated = false;
        //if (buttonPressed == buttons[weaponController.ActualIndex])
        if (buttonPressed == comboStruct[actualIndex].button && isActive)
        {
            //Debug.Log("#COMBO# Combo Started: " + this.name + " button: " + buttons[weaponController.ActualIndex]);
            activated = true;

            //++weaponController.ActualIndex;
            if (comboStruct[actualIndex].hasDash)
            {
                DoDash();
            }
            
        }

        return activated;
    }

    public async Task UseSkill(CompanyCharacterController owner, CompanyCharacterController target)
    {
        comboStruct[actualIndex++].skill.ProcessSkill(owner, target);
        if (weaponController.ActualIndex == comboStruct.Count)
        {
            actualIndex = 0;
        }
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
            if(isActive && actualActionStack[i] != comboStruct[i].button)
            {
                isThisCombo = false;
                break;
            }
        }

        if(isThisCombo && buttonPressed == comboStruct[weaponController.ActualIndex].button)
        {
            if (comboStruct[actualIndex].hasDash)
            {
                DoDash();
            }
            
            if(weaponController.ActualIndex == comboStruct.Count-1)
            {
                weaponController.ContinueAnimationCombo();
                comboFinished = true;
                //weaponController.FinishCombo();
                //Debug.Log("#COMBO# Combo Finished: " + this.name + " button: " + buttons[weaponController.ActualIndex]);
                //actualIndex = 0;
            }
            else
            {
                comboContinued = true;
                //Debug.Log("#COMBO# Combo Continued: " + this.name + " actualIndex = " + weaponController.ActualIndex + " button: " + buttons[weaponController.ActualIndex]);
                //++weaponController.ActualIndex;
            }

            weaponController.DoingCombo = true;
        }
        else
        {
            actualIndex = 0;
        }

        return comboContinued;
    }

    public int GetComboLength()
    {
        return comboStruct.Count;
    }

    private void DoDash()
    {
        GameManager.Instance.GetPlayerController().DoDash(comboStruct[actualIndex].dashTime, comboStruct[actualIndex].dashPower);
    }
}
