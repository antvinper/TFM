using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectItem : Item
{
    [SerializeField] protected InstantEffectTemporally effect;

    public string GetEffectName()
    {
        return this.effect.EffectName;
    }

    public string GetEffectDescription()
    {
        return this.effect.Description;
    }

    public int GetEffectValueInPercentage()
    {
        return this.effect.ValueInPercentage;
    }

    public void UseItem(Characters.CharacterController target)
    {
        effect.ProcessEffect(target);
    }
}
