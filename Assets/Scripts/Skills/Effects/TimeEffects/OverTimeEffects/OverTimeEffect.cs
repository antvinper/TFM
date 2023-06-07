using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

//[Serializable]
public abstract class OverTimeEffect : TimeEffectDefinition
{
    /**
     * Aplica el efecto cada x tiempo.
     * Por ejemplo. Si es tipo veneno, causará daño cada x tiempo
     * Si es tipo curación, se irá curando x cantidad cada cierto tiempo.
     */
    [SerializeField] protected float timeBetweenApplyEffect;
    
    public override async Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
        Debug.Log("Before apply the Effect, " + statAffected + " " + target.GetStat(statAffected));

        float timeAplyingEffect = 0.0f;
        float actualTimeBetweenApplyEffect = 0.0f;

        

        Debug.Log("After apply the slowDownapply the Effect, " + statAffected + " " + target.GetStat(statAffected));

        while(timeAplyingEffect < effectTime)
        {
            Debug.Log("Time = " + timeAplyingEffect);

            if(actualTimeBetweenApplyEffect >= timeBetweenApplyEffect)
            {
                ApplyEffect(target);
                actualTimeBetweenApplyEffect = 0.0f;
            }
            timeAplyingEffect += Time.deltaTime;
            actualTimeBetweenApplyEffect += Time.deltaTime;

            await new WaitForSeconds(Time.deltaTime);
        }

        Debug.Log("Finally apply the Effect, " + statAffected + " " + target.GetStat(statAffected));
    }

    private void ApplyEffect(Characters.CharacterController target)
    {
        if (isPositive)
        {
            target.SetStat(statAffected, (target.GetStat(statAffected) + valueInPercentage));
        }
        else
        {
            target.SetStat(statAffected, (target.GetStat(statAffected) - valueInPercentage));
        }
    }
}