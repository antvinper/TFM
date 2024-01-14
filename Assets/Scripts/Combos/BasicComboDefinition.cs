using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicCombo", menuName = "TFM/Combos/Basic Combo Definition")]
public class BasicComboDefinition : ComboDefinition
{
    int actualIndex = 0;

    public void SetUp()
    {
        Reset();
        isActive = startsActive;
    }

    public void Reset()
    {
        actualIndex = 0;
        isRunning = false;
    }

    public bool StartCombo(ButtonsXbox buttonPressed)
    {
        bool activated = false;
        if (buttonPressed == comboStruct[actualIndex].button && isActive)
        {
            activated = true;
            isRunning = true;

            if (comboStruct[actualIndex].hasDash)
            {
                DoDash();
            }
            
        }

        return activated;
    }

    public async Task UseSkill(CompanyCharacterController owner, CompanyCharacterController target, WeaponController weaponController)
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
        return actualIndex == comboStruct.Count -1 ? true : false;
    }

    public bool ContinueCombo(ButtonsXbox buttonPressed, List<ButtonsXbox> actualActionStack, WeaponController weaponController)
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
                }
                else
                {
                    comboContinued = true;
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
        GameManager.Instance.GetPlayerController().DoDash(comboStruct[actualIndex].dashTime, comboStruct[actualIndex].dashPower);
    }
}
