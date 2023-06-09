using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

//[Serializable]
public abstract class DuringTimeEffect : TimeEffectDefinition
{
    public override async Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
        Debug.Log("Before apply the Effect, " + statAffected + " " + target.GetStat(statAffected));

        float timeAplyingEffect = 0.0f;

        float preValue = target.GetStat(statAffected);
        float finalValue;
            
        /**
         * TODO
         * refactor in a new method
         */
        if (isPositive)
        {
            finalValue = preValue + value;
        }
        else
        {
            finalValue = preValue - value;
        }

        StatModificator statModificator = new StatModificator(statAffected, finalValue, isValueInPercentage, false);
        target.ChangeStat(statModificator);

        Debug.Log("After apply the slowDownapply the Effect, " + statAffected + " " + target.GetStat(statAffected));

        //Lo dejo así por si se desea dar feedback del tiempo o algo
        while (timeAplyingEffect < effectTime)
        {
            Debug.Log("Time = " + timeAplyingEffect);
            timeAplyingEffect += Time.deltaTime;
            await new WaitForSeconds(Time.deltaTime);
        }

        //Siempre será isPercentual a false, porque tiene que devolver el valor que había en un inicio.
        statModificator = new StatModificator(statAffected, preValue, false, false);
        target.ChangeStat(statModificator);

        Debug.Log("Finally apply the Effect, " + statAffected + " " + target.GetStat(statAffected));
    }
}