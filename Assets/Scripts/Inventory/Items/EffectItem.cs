using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectItem : Item
{
    [SerializeField] private InstantEffectTemporally effect;

    public void Setup()
    {
        finalPrice = price;
        nameSufix = "";

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
