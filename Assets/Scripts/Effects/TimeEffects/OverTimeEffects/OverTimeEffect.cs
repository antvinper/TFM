using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

//[Serializable]
public class OverTimeEffect : TimeEffectDefinition
{
    /**
     * Aplica el efecto cada x tiempo.
     * Por ejemplo. Si es tipo veneno, causar� da�o cada x tiempo
     * Si es tipo curaci�n, se ir� curando x cantidad cada cierto tiempo.
     */
    [SerializeField] protected float timeBetweenApplyEffect;


    public override async Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
        if (target.TryAddTemporallyState(this))
        {
            Debug.Log("Before apply the Effect, " + statAffected + " " + target.GetStat(statAffected));

            float timeAplyingEffect = 0.0f;
            float actualTimeBetweenApplyEffect = 0.0f;



            Debug.Log("After apply the the Effect, " + statAffected + " " + target.GetStat(statAffected));

            while (timeAplyingEffect < effectTime && !cancel)
            {
                if (reset)
                {
                    timeAplyingEffect = 0.0f;
                    actualTimeBetweenApplyEffect = 0.0f;
                    reset = false;
                }
                Debug.Log("Time = " + timeAplyingEffect);

                if (actualTimeBetweenApplyEffect >= timeBetweenApplyEffect)
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
        else
        {
            Debug.Log(statAffected + " couldn't be applied");
        }
    }

    /**
     * TODO
     */
    public override void Cancel()
    {
        throw new NotImplementedException();
    }

    private void ApplyEffect(Characters.CharacterController target)
    {
        float finalValue;
        /**
         * TODO
         * refactor the if else in a new method to get the finalValue
         */
        if (isPositive)
        {
            finalValue = target.GetStat(statAffected) + valueInPercentage;
        }
        else
        {
            finalValue = target.GetStat(statAffected) - valueInPercentage;
        }
        StatModificator statModificator = new StatModificator(statAffected, finalValue, isValueInPercentage, false);
        target.ChangeStat(statModificator);
    }
}