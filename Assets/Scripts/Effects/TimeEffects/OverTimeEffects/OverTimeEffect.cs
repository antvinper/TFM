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
            Debug.Log("Before apply the Effect, " + StatAffected + " " + target.GetStat(StatAffected));

            float timeAplyingEffect = 0.0f;
            float actualTimeBetweenApplyEffect = 0.0f;
            
            /**
             * TODO
             * Calcular el porcentaje bien
             * Si tengo veneno y ese veneno me quita un 10% de la vida,
             * tiene que ser un 10% de la vida máxima. 
             * Lo hace??
             * 
             */

            while (timeAplyingEffect < effectLifeTime && !cancel)
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
                    Debug.Log("After apply the the Effect, " + StatAffected + " " + target.GetStat(StatAffected));
                    actualTimeBetweenApplyEffect = 0.0f;
                }
                timeAplyingEffect += Time.deltaTime;
                actualTimeBetweenApplyEffect += Time.deltaTime;

                await new WaitForSeconds(Time.deltaTime);
                
                
            }

            Debug.Log("Finally apply the Effect, " + StatAffected + " " + target.GetStat(StatAffected));
        }
        else
        {
            Debug.Log(name + " couldn't be applied");
        }
    }

    
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
            if (IsStatIncremented)
            {
                finalValue = target.GetStat(StatAffected) + valueInPercentage;
            }
            else
            {
                finalValue = target.GetStat(StatAffected) - valueInPercentage;
            }
        }
        else
        {
            /**
             * TODO
             * Obtener el valor bien de value o de un stat en concreto del owner
             * De momento vamos a probar que va con valor
             */
            if (IsStatIncremented)
            {
                finalValue = Value;
            }
            else
            {
                finalValue = -Value;
            }
        }
        
        StatModificator statModificator = new StatModificator(StatAffected, finalValue, isValueInPercentage, false, effectType);
        target.ChangeStat(statModificator);
    }

    public override Task ProcessEffect(Characters.CharacterController target)
    {
        return Task.CompletedTask;
    }
}