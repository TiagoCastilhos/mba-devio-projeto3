namespace Coldmart.Core;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTimeOffset DataCriacao { get; protected set; }
    public bool Deletado { get; protected set; }
}