using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InstantEffectTemporally : EffectDefinition
{
    public override Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
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

        int finalValue = IsStatIncremented ? valueInPercentage : -valueInPercentage;
        StatModificator statModificator = new StatModificator(StatAffected, finalValue, true, false);
        target.ChangeStat(statModificator);

        Debug.Log(StatAffected + " now is: " + target.GetStat(StatAffected));
    }

    private void ProcessEffectInReal(Characters.CharacterController owner, Characters.CharacterController target)
    {
        int finalValue = IsStatIncremented ? Value : -Value;

        StatModificator statModificator = new StatModificator(StatAffected, finalValue, false, false);
        target.ChangeStat(statModificator);

        Debug.Log(StatAffected + " now is: " + target.GetStat(StatAffected));
    }
}
