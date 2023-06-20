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
    private int finalValue;
    public override async Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
        //No sé por qué valen true nada más empezar
        cancel = false;
        reset = false;
        this.owner = owner;
        this.target = target;

        if (target.TryAddTemporallyState(this))
        {
            Debug.Log("Before apply the Effect, " + StatAffected + " " + target.GetStat(StatAffected));

            float timeAplyingEffect = 0.0f;

            /**
             * TODO
             * ADD PERCENTUAL CHANGE
             * Puede ser que se le reduzca la velocidad un 10%, la defensa un 15%
             * Pero siempre del total sin tener en cuenta los cambios temporales.
             * Si se reduce la defensa 2 veces un 10%, siempre será respecto al valor Permanent
             * 
             * ¿¿Como puedo saber si obtener el valor del actual o del permanente? Elección de momento.
             */
            
           /* if(isValueInPercentage)
            {
                finalValue = GetPercentageValue(this.owner, this.target);
            }
            else
            {
                finalValue = GetValue();
            }*/

            finalValue = GetValue();


            ChangeStat(finalValue, isValueInPercentage);
            Debug.Log("After apply the slowDownapply the Effect, " + StatAffected + " " + target.GetStat(StatAffected));

            //Lo dejo así por si se desea dar feedback del tiempo o algo
            while (timeAplyingEffect < effectLifeTime && !cancel)
            {
                if (reset)
                {
                    timeAplyingEffect = 0.0f;
                    reset = false;
                }
                timeAplyingEffect += Time.deltaTime;

                
                await new WaitForSeconds(Time.deltaTime);
            }

            /**
             * TODO hay qye comprobarlo bien todo
             */
            if (!cancel)
            {
                //Siempre será isPercentual a false, porque tiene que devolver el valor que había en un inicio.
                ChangeStat(-finalValue, false);

                Debug.Log("Finally apply the Effect, " + StatAffected + " " + target.GetStat(StatAffected));
            }
           
        }
        else
        {
            Debug.Log(StatAffected + " couldn't be applied");
        }
        
    }

    public override void Cancel()
    {
        cancel = true;
        ChangeStat(-finalValue, false);
        Debug.Log("Cancelled apply the Effect, " + StatAffected + " " + target.GetStat(StatAffected));
    }

    private void ChangeStat(int value, bool isPercentual)
    {
        StatModificator statModificator = new StatModificator(StatAffected, value, isPercentual, false);
        target.ChangeStat(statModificator);
    }

    public override Task ProcessEffect(Characters.CharacterController target)
    {
        return Task.CompletedTask;
    }
}