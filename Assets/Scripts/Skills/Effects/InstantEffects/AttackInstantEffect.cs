using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class AttackInstantEffect : InstantEffectDefinition
{
    public override async Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
        Debug.Log("Processing AttackInstantEffect");

        target.SetStat(statAffected, owner.GetMyRealDamage());
    }

}
