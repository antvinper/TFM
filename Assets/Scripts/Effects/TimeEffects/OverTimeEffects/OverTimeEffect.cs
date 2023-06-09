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
     * Por ejemplo. Si es tipo veneno, causará daño cada x tiempo
     * Si es tipo curación, se irá curando x cantidad cada cierto tiempo.
     */
    [SerializeField]
    [Range(0f, float.MaxValue)] 
    protected float timeBetweenApplyEffect;

    public float TimeBetweenApplyEffect { get => timeBetweenApplyEffect; }


    public override async Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
        if (target.TryAddTemporallyState(this))
        {
            Debug.Log(name + " has been applied");
            cancel = false;
            reset = false;
            Debug.Log("Before apply the Effect, " + statAffected + " " + target.GetStat(statAffected));

            float timeAplyingEffect = 0.0f;
            float actualTimeBetweenApplyEffect = 0.0f;
            

            while (timeAplyingEffect < effectTime && !cancel)
            {
                if (reset)
                {
                    Debug.Log(this.name + " has been reseted");
                    timeAplyingEffect = 0.0f;
                    actualTimeBetweenApplyEffect = 0.0f;
                    reset = false;
                }

                if (actualTimeBetweenApplyEffect >= timeBetweenApplyEffect)
                {
                    ApplyEffect(target);
                    Debug.Log("After apply the the Effect, " + statAffected + " " + target.GetStat(statAffected));
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
            Debug.Log(name + " couldn't be applied");
        }
    }

    /**
     * TODO
     */
    public override void Cancel()
    {
        cancel = true;
        Debug.Log(this.name + " cancelled.");
    }

    private void ApplyEffect(Characters.CharacterController target)
    {
        float finalValue;
        /**
         * TODO
         * refactor the if else in a new method to get the finalValue
         */
        if (isValueInPercentage)
        {
            if (isPositive)
            {
                finalValue = target.GetStat(statAffected) + valueInPercentage;
            }
            else
            {
                finalValue = target.GetStat(statAffected) - valueInPercentage;
            }
        }
        else
        {
            /**
             * TODO
             * Obtener el valor bien de value o de un stat en concreto del owner
             * De momento vamos a probar que va con valor
             */
            if (isPositive)
            {
                finalValue = value;
            }
            else
            {
                finalValue = -value;
            }
        }
        
        StatModificator statModificator = new StatModificator(statAffected, finalValue, isValueInPercentage, false, buffDebuffType);
        target.ChangeStat(statModificator);
    }
}