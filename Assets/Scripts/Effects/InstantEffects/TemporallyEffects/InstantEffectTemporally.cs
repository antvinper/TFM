using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InstantEffectTemporally : EffectDefinition
{
    public override Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
        this.owner = owner;
        this.target = target;
        if (isValueInPercentage)
        {
            ProcessEffectInPercentage(owner, target);
        }
        else
        {
            ProcessEffectInReal(owner, target);
        }
        

        return Task.CompletedTask;
    }

    public override Task ProcessEffect(Characters.CharacterController target)
    {
        return Task.CompletedTask;
    }

    private void ProcessEffectInPercentage(Characters.CharacterController owner, Characters.CharacterController target)
    {
        int finalValue = GetPercentageValue(owner, target);
        StatModificator statModificator = new StatModificator(StatAffected, finalValue, true, false);

        ChangeStat(statModificator);

    }

    private void ProcessEffectInReal(Characters.CharacterController owner, Characters.CharacterController target)
    {
        int finalValue = IsStatIncremented ? Value : -Value;

        StatModificator statModificator = new StatModificator(StatAffected, finalValue, false, false);

        ChangeStat(statModificator);
        
    }

    public override async Task RemoveEffect()
    {
        //TODO
    }
}
