using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class HealInstantEffect : InstantEffectDefinition
{
    public override async Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
        Debug.Log("Processing AttackInstantEffect");

        target.SetStat(statAffected, owner.GetMyRealDamage());
    }
}
