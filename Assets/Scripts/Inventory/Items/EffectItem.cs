using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectItem : Item
{
    [SerializeField] protected InstantEffectTemporally effect;

    public void Setup()
    {
        //name = GetEffectName();
        finalPrice = price;
        nameSufix = "";

        if (IsValueInPercentage())
        {
            int value = GetEffectValueInPercentage();
            //nameSufix = ": " + value + "%";
        }
        else
        {
            int value = GetEffectValue();
            //nameSufix = ": " + value;
        }

        description = GetEffectDescription();
    }

    public string GetEffectName()
    {
        return effect.EffectName;
    }

    public string GetEffectDescription()
    {
        return effect.Description;
    }

    public int GetEffectValueInPercentage()
    {
        return effect.ValueInPercentage;
    }
    public int GetEffectValue()
    {
        return effect.Value;
    }

    public bool IsValueInPercentage()
    {
        return effect.IsValueInPercentage;
    }

    public void UseItem(Characters.CharacterController target)
    {
        effect.ProcessEffect(target);
    }
}
