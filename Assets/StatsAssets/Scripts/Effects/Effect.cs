namespace CompanyStats
{
    public class Effect
    {
        protected EffectDefinition effectDefinition;
        public EffectDefinition EffectDefinition => effectDefinition;
        protected CompanyCharacterController target;
        protected CompanyCharacterController owner;
        public Effect(EffectDefinition effectDefinition, CompanyCharacterController target, CompanyCharacterController owner)
        {
            this.effectDefinition = effectDefinition;
            this.target = target;
            this.owner = owner;
        }

        public virtual void ProcessEffect() { }
    }
}


