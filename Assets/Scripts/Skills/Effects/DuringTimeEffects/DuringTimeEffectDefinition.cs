using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "DuringTimeEffectDefinition", menuName = "Effects/DuringTimeEffects/During Time Effect Definition")]
public class DuringTimeEffectDefinition : DuringTimeEffect
{
    public override async Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
        Debug.Log("Before apply the slowDown, speed = " + target.GetStat(statAffected));

        float timeAplyingEffect = 0.0f;

        float preValue = target.GetStat(statAffected);

        if(isPositive)
        {
            target.SetStat(statAffected, (preValue + reductionValue));
        } 
        else
        {
            target.SetStat(statAffected, (preValue - reductionValue));
        }
        

        Debug.Log("After apply the slowDown, speed = " + target.GetStat(statAffected));

        //Lo dejo así por si se desea dar feedback del tiempo o algo
        while(timeAplyingEffect < effectTime)
        {
            Debug.Log("Time = " + timeAplyingEffect);
            timeAplyingEffect += Time.deltaTime;
            await new WaitForSeconds(Time.deltaTime);
        }

        target.SetStat(statAffected, preValue);

        Debug.Log("Finally speed = " + target.GetStat(statAffected));
    }
}
