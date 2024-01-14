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
        Reset();
        this.weaponController = weaponController;
        isActive = startsActive;
    }

    public void Reset()
    {
        //Debug.Log("#MOVE Reset actual index in " + name);
        actualIndex = 0;
        isRunning = false;
    }

    public bool StartCombo(ButtonsXbox buttonPressed)
    {
        bool activated = false;
        //if (buttonPressed == buttons[weaponController.ActualIndex])
        if (buttonPressed == comboStruct[actualIndex].button && isActive)
        {
            //Debug.Log("#COMBO# Combo Started: " + this.name + " button: " + buttons[weaponController.ActualIndex]);
            activated = true;
            isRunning = true;
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
        comboStruct[actualIndex].skill.ProcessSkill(owner, target);
        if (weaponController.ActualIndex == comboStruct.Count)
        {
            Reset();
        }
    }

    internal void SetActive(bool isActive)
    {
        this.isActive = isActive;
    }

    public bool IsLastComboIndex()
    {
        //Debug.Log("#MOVE actualIndex in " + name +": " + actualIndex);
        return actualIndex == comboStruct.Count -1 ? true : false;
    }

    public bool ContinueCombo(ButtonsXbox buttonPressed, List<ButtonsXbox> actualActionStack)
    {
        bool comboContinued = false;
        if (isRunning)
        {
            bool isThisCombo = true;
            for (int i = 0; i < actualActionStack.Count; ++i)
            {
                if (isActive && actualActionStack[i] != comboStruct[i].button)
                {
                    isThisCombo = false;
                    break;
                }
            }

            if (isThisCombo && buttonPressed == comboStruct[weaponController.ActualIndex].button)
            {
                ++actualIndex;
                if (comboStruct[actualIndex].hasDash)
                {
                    DoDash();
                }

                if (weaponController.ActualIndex == comboStruct.Count - 1)
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
                Reset();
            }
        }

        return comboContinued;
    }

    public int GetComboLength()
    {
        return comboStruct.Count;
    }

    private void DoDash()
    {
        Debug.Log("DO DASH");
        GameManager.Instance.GetPlayerController().DoDash(comboStruct[actualIndex].dashTime, comboStruct[actualIndex].dashPower);
    }
}
