using UnityEngine;

namespace CompanyStats
{
    public class AttackEffect : InstantEffect
    {
        public AttackEffect(EffectDefinition effectDefinition, CompanyCharacterController target, CompanyCharacterController owner) : base(effectDefinition, target, owner)
        {

        }

        public override void ProcessEffect()
        {
            Debug.Log(this.effectDefinition.EffectName);

            if (effectDefinition.IsValueInPercentage)
            {
                effectDefinition.CalculateRealPercentage();
            }

            Strike strike = new Strike(owner, target, effectDefinition);
            strike.ProcessStrike();
            target.ApplyDamage(strike);
        }
    }
}

