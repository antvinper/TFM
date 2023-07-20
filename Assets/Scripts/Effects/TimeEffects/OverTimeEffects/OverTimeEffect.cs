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
        this.owner = owner;
        this.target = isTheOwnerStat ? owner : target;
        //this.target = target;
        if (target.TryAddTemporallyState(this))
        {
            Debug.Log(name + " has been applied");
            cancel = false;
            reset = false;
            //Debug.Log("Before apply the Effect, " + StatAffected + " " + target.GetStat(StatAffected));

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
                   // Debug.Log("After apply the the Effect, " + StatAffected + " " + target.GetStat(StatAffected));
                    actualTimeBetweenApplyEffect = 0.0f;
                }
                timeAplyingEffect += Time.deltaTime;
                actualTimeBetweenApplyEffect += Time.deltaTime;

                await new WaitForSeconds(Time.deltaTime);
                
                
            }
            if (ApplyOnSelf)
            {
                Debug.Log("Finally apply the Effect, " + name + " " + owner.GetStat(StatAffected));
            }
            else
            {
                Debug.Log("Finally apply the Effect, " + name + " " + target.GetStat(StatAffected));
            }
            
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
        int finalValue;
        /**
         * TODO
         * refactor the if else in a new method to get the finalValue
         */
        if (isValueInPercentage)
        {
            finalValue = GetPercentageValue(this.owner, this.target);
        }
        else
        {
            finalValue = GetValue();
        }
        /*if (isValueInPercentage)
        {
            //1- Obtener valor del statwhat to see. Si es la maxHealth = 100
            finalValue = target.GetStat(statWhatToSee);
            //2- Saco el valor porcentaje. Si el porcentaje es 5: finalValue = 100*0.05 = 5
            float percentual = valueInPercentage * 0.01f;
            finalValue = Mathf.RoundToInt(IsStatIncremented ? finalValue * percentual : finalValue * -percentual);


            finalValue = GetPercentageValueFromActualStat(owner, target);
        }
        else
        {
            if (IsStatIncremented)
            {
                finalValue = Value;
            }
            else
            {
                finalValue = -Value;
            }
        }*/

        //Siempre será false, puesto que queremos aplicar un efecto instantáneo.
        //Cómo se va a usar para reducir la vida o aumentarla, no va a volver a su estado inicial.
        ChangeStatOTE(finalValue, false);
        /*StatModificator statModificator = new StatModificator(StatAffected, (int)finalValue, isValueInPercentage, false, effectType);
        target.ChangeStat(statModificator);*/
    }

    private void ChangeStatOTE(int value, bool isPercentual)
    {
        StatModificator statModificator = new StatModificator(StatAffected, value, isPercentual, false, effectType);
        //target.ChangeStat(statModificator);
        ChangeStat(statModificator);
    }

    public override Task ProcessEffect(Characters.CharacterController target)
    {
        return Task.CompletedTask;
    }
}