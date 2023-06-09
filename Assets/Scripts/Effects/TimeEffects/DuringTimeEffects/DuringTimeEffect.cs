using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

//[Serializable]
public class DuringTimeEffect : TimeEffectDefinition
{
    private Characters.CharacterController owner, target;
    private float finalValue;
    public override async Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
        //No sé por qué valen true nada más empezar
        cancel = false;
        reset = false;
        this.owner = owner;
        this.target = target;

        if (target.TryAddTemporallyState(this))
        {
            Debug.Log("Before apply the Effect, " + statAffected + " " + target.GetStat(statAffected));

            float timeAplyingEffect = 0.0f;

            /**
             * TODO
             * refactor in a new method
             */
            if (isPositive)
            {
                finalValue = value;
            }
            else
            {
                finalValue = -value;
            }

            ChangeStat(finalValue, isValueInPercentage);
            Debug.Log("After apply the slowDownapply the Effect, " + statAffected + " " + target.GetStat(statAffected));

            //Lo dejo así por si se desea dar feedback del tiempo o algo
            while (timeAplyingEffect < effectTime && !cancel)
            {
                if (reset)
                {
                    timeAplyingEffect = 0.0f;
                    reset = false;
                }
                timeAplyingEffect += Time.deltaTime;
                await new WaitForSeconds(Time.deltaTime);
            }

            if (!cancel)
            {
                //Siempre será isPercentual a false, porque tiene que devolver el valor que había en un inicio.
                ChangeStat(-finalValue, false);

                Debug.Log("Finally apply the Effect, " + statAffected + " " + target.GetStat(statAffected));
            }
           
        }
        else
        {
            Debug.Log(statAffected + " couldn't be applied");
        }
        
    }

    public override void Cancel()
    {
        cancel = true;
        ChangeStat(-finalValue, false);
        Debug.Log("Finally apply the Effect, " + statAffected + " " + target.GetStat(statAffected));
    }

    private void ChangeStat(float value, bool isPercentual)
    {
        StatModificator statModificator = new StatModificator(statAffected, value, isPercentual, false);
        target.ChangeStat(statModificator);
    }
}