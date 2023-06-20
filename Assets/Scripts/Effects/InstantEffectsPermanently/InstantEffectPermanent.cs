using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InstantEffectPermanent : EffectDefinition
{
    public override Task ProcessEffect(Characters.CharacterController target)
    {

        //Es permanente, siempre es incrementable.
        /**
         * TODO
         * Los stats que aumentan de manera permanente aumentan en valor fijo, nunca porcentualmente.
         * Es decir, en el árbol mejora se aumentarán por ejemplo 100 de vida.
         * Pero luego en el juego las mejoras que vayan dando en la run o que te equipes
         * podrán ser porcentuales o fijas.
         */

        StatModificator statModificator = new StatModificator(StatAffected, Value, isValueInPercentage, true);
        target.ChangeStat(statModificator);
        /*if (StatAffected.Equals(StatsEnum.MAX_HEALTH))
        {
            statModificator = new StatModificator(StatsEnum.HEALTH, Value, isValueInPercentage, true);
            target.ChangeStat(statModificator);
        }*/
        

        return Task.CompletedTask;
    }

    public override Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
        return Task.CompletedTask;
    }
}
