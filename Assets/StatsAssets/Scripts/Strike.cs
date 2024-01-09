using UnityEngine;

namespace CompanyStats
{
    public class Strike
    {
        CompanyCharacterController owner;
        CompanyCharacterController target;
        EffectDefinition effect;

        int finalValue;
        public int FinalValue { get => finalValue; }
        public EffectDefinition Effect { get => effect;}

        public Strike(CompanyCharacterController owner, CompanyCharacterController target, EffectDefinition effect)
        {
            this.owner = owner;
            this.target = target;
            this.effect = effect;
        }

        public void ProcessStrike()
        {
            int deffense = 0;
            switch (effect.EffectType)
            {
                case EffectTypesEnum.ATTACK:
                    deffense = target.GetStatValue(StatNames.DEFENSE, effect.StatPart);
                    break;
                case EffectTypesEnum.MAGIC:
                    deffense = target.GetStatValue(StatNames.MAGICAL_DEFENSE, effect.StatPart);
                    break;
                case EffectTypesEnum.POISON:
                    Debug.Log("TODO something here?");
                    break;
            }
            //Debug.Log("TODO -> Apply dodge, critial, evasion...");
            //Debug.Log("TODO -> Si tal buscar de hacer lo de la postura. Cuando se le rompa la postura, pierde stats y ganas facilidad de crítico....");

            int preValue = Mathf.Abs(effect.Value);
            if (!effect.IsValueInPercentage)
            {
                preValue = Mathf.Abs(effect.Value) - deffense;
            }
            finalValue = preValue > 0 ? preValue : 0;
        }
}
}

