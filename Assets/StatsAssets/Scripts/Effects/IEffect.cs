namespace CompanyStats
{
    public interface IEffect
    {
        public string EffectName { get; }
        public string Description { get; }
        public bool ApplyOnSelf { get; }
        public bool IsStatIncremented { get; }
        public StatNames StatAffected { get; }
        public StatParts StatPart { get; }
        public int Value { get; }
    }
}
