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
        /**
         * TODO
         * Introducir sólo el valor porcentual, es decir, entre 1 y 100
         * ya me encargo luego de calcular el valor al obtenerlo
         */



        /*float statValue = isTheOwnerStat ? owner.GetStat(statWhatToSee) : target.GetStat(statWhatToSee);

        float finalValueInPercentage = (statValue / 100) * valueInPercentage;

        if (!IsStatIncremented)
        {
            finalValueInPercentage = -finalValueInPercentage;

        }

        //IsPermanent siempre será false puesto que es forRun
        StatModificator statModificator = new StatModificator(StatAffected, finalValueInPercentage, true, false);
        target.ChangeStat(statModificator);

        Debug.Log(StatAffected + " now is: " + target.GetStat(StatAffected));*/

        int finalValue = GetPercentageValue(owner, target);
        //int finalValue = IsStatIncremented ? valueInPercentage : -valueInPercentage;
        StatModificator statModificator = new StatModificator(StatAffected, finalValue, true, false);

        ChangeStat(statModificator);

        //target.ChangeStat(statModificator);

        //Debug.Log(StatAffected + " now is: " + target.GetStat(StatAffected));
    }

    private void ProcessEffectInReal(Characters.CharacterController owner, Characters.CharacterController target)
    {
        int finalValue = IsStatIncremented ? Value : -Value;

        StatModificator statModificator = new StatModificator(StatAffected, finalValue, false, false);

        ChangeStat(statModificator);
        /*if (ApplyOnSelf)
        {
            Debug.Log(StatAffected + " from " + owner.name + " before apply effect is: " + owner.GetStat(StatAffected));
            owner.ChangeStat(statModificator);
            Debug.Log(StatAffected + " from " + owner.name +" now is: " + owner.GetStat(StatAffected));
        }
        else
        {
            Debug.Log(StatAffected + " from " + target.name + " before apply effect is: " + target.GetStat(StatAffected));
            target.ChangeStat(statModificator);
            Debug.Log(StatAffected + " from " + target.name + " now is: " + target.GetStat(StatAffected));
        }*/
        
    }
}
