using System.Threading.Tasks;

namespace CompanyStats
{
    /**
     * Son aquellos efectos que se aplican siempre debido a los siguientes supuestos:
     * - Se equipa un arma, armadura...
     * - Se aplica un ataque.
     * - Se aplica una curación.
     * - Alguno más??
     */
    public class InstantEffectTemporally : TemporallyEffectDefinition
    {
        public override async Task ProcessEffect(CompanyCharacterController owner, CompanyCharacterController target)
        {
            this.target = target;
            this.owner = owner;
            //TODO
            // Convertir en factory
            Effect effect = null;
            switch (this.EffectType)
            {
                case EffectTypesEnum.ATTACK:
                    effect = new AttackEffect(this, target, owner);
                    break;
                case EffectTypesEnum.HEAL:
                    effect = new HealEffect(this, target, owner);
                    break;
                default:
                    effect = new InstantEffect(this, target, owner);
                    break;
            }
            effect.ProcessEffect();
        }

        public override async Task RemoveEffect(CompanyCharacterController target)
        {
            target.TryRemoveEffect(this);
        }

        public override async Task RemoveEffect()
        {
            target.TryRemoveEffect(this);
        }

    }
}

