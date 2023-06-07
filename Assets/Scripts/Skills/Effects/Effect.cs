using System.Threading.Tasks;

public abstract class Effect
{
    public abstract string Id { get; }
    public abstract string Description { get; }

    public abstract Task ExecuteAsync();

    public bool Equals(Effect other)
    {
        return this.Id.Equals(other.Id);
    }
}
