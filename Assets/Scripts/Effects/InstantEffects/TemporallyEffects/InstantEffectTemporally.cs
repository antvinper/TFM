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
        this.owner = target;
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

    private void ProcessEffectInPercentage(Characters.CharacterController owner, Characters.CharacterController target)
    {
        int finalValueInPercentage = GetPercentageValue(owner, target);
        StatModificator statModificator;
        if (statWhatToSee != StatsEnum.MAX_HEALTH && statWhatToSee != StatsEnum.HEALTH)
        {
            int valueSWTS = isTheOwnerStat ? owner.GetStat(statWhatToSee) : target.GetStat(statWhatToSee);
            int finalValue = (int)((valueSWTS * 0.01f) * finalValueInPercentage);
            Debug.Log("Value: " + finalValue);
            statModificator = new StatModificator(StatAffected, finalValue, false, false);
        }
        else
        {
            statModificator = new StatModificator(StatAffected, finalValueInPercentage, true, false);
        }

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
