using System.Collections.Generic;
using UnityEngine;

namespace CompanyStats
{
    public class InstantEffect : Effect
    {
        public InstantEffect(EffectDefinition effectDefinition, CompanyCharacterController target, CompanyCharacterController owner) : base(effectDefinition, target, owner)
        {
        }

        public override void ProcessEffect()
        {
            Debug.Log("Processing instant effect.");
            //target.ApplyInstantEffect(this.effectDefinition);
            //Debug.Log("TODO -> ProcessEffect from not rechargable stats");

            Stat stat = target.GetStatFromName(effectDefinition.StatAffected);
            Debug.Log(stat.StatName + "." + effectDefinition.StatPart + " value before apply the effect "+ effectDefinition.name +": " + target.GetStatValue(stat.StatName, effectDefinition.StatPart));

            /*if (effectDefinition.IsValueInPercentage)
            {
                Stat s = effectDefinition.IsTheOwnerStat ?
                    owner.GetStatFromName(effectDefinition.StatWhatToSee) :
                    target.GetStatFromName(effectDefinition.StatWhatToSee);
                
                //Calcular el porcentaje
            }*/

            if (target.TryAddEffect(effectDefinition, owner))
            {
                effectDefinition.HasBeenApplied = true;
                Debug.Log("Effect applied correctly");
                Debug.Log(stat.StatName + "." + effectDefinition.StatPart + " value after apply the effect " + effectDefinition.name + ": " + target.GetStatValue(stat.StatName, effectDefinition.StatPart));
            }
            else
            {
                Debug.Log("Effect couldn't be added");
            }
        }

        protected void ChangeStatInstantly(EffectDefinition effect)
        {
            switch (effectDefinition.StatPart)
            {
                case StatParts.ACTUAL_VALUE:
                    target.ChangeActualStatInstantly(effectDefinition);
                    break;
                case StatParts.ACTUAL_MAX_VALUE:
                    target.ChangeActualMaxStatInstantly(effectDefinition);
                    break;
            }
        }

    }

}
