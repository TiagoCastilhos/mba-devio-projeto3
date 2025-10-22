namespace Coldmart.Core.Domain;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTimeOffset DataCriacao { get; set; }
    public bool Deletado { get; set; }

    protected Entity() { }

    public Entity(Guid id)
    {
        Id = id;
        DataCriacao = DateTimeOffset.UtcNow;
        Deletado = false;
    }
}