using System.Threading.Tasks;

public interface IEffect
{
    public string EffectName { get; }
    public string Description { get; }
    public bool IsStatIncremented { get; }
    public StatsEnum StatAffected { get; }
    public int Value { get; }
}
