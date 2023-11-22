using UnityEngine;

namespace CompanyStats
{
    public class HealEffect : InstantEffect
    {
        public HealEffect(EffectDefinition effectDefinition, CompanyCharacterController target, CompanyCharacterController owner) : base(effectDefinition, target, owner)
        {

        }

        public override void ProcessEffect()
        {
            Debug.Log("Processing heal effect.");
            /*switch (effectDefinition.StatAffected)
            {
                case StatNames.HEALTH:
                    // TODO
                    break;
                case StatNames.MANA:
                    // TODO
                    break;
            }*/
            //target.ApplyInstantEffect(effectDefinition);
            Stat stat = target.GetStatFromName(effectDefinition.StatAffected);
            Debug.Log(effectDefinition.StatAffected +"."+ effectDefinition.StatPart + " before apply the effect " + effectDefinition.name + ": " + stat.Value);
            this.ChangeStatInstantly(effectDefinition);
            Debug.Log(effectDefinition.StatAffected + "." + effectDefinition.StatPart + " after apply the effect " + effectDefinition.name + ": " + stat.Value);
        }

    }
}

