using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackEffectTest", menuName = "Effects/Attack Effect Definition Test")]
public class AttackInstantEffect : InstantEffect
{
    public AttackInstantEffect()
    {
        effectType = EffectType.ATTACK;
    }

    //public override async Task ProcessEffect()
    /*public override async Task ProcessEffect()
    {
        Debug.Log("AttackInstantEffect in action");
    }*/
    public override async Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
        //Hay que pasarle al controller lo que toque. En este caso hay que hacerle daño.

        //target.ProcessDamage(1);
        Debug.Log("Processing AttackInstantEffect");
        target.ProcessDamage(owner.GetMyRealDamage());
    }
}
