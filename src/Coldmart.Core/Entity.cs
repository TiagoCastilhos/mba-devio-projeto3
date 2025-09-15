namespace Coldmart.Core;

public abstract class Entity
{
    public int Id { get; set; }
    public DateTimeOffset DataCriacao { get; set; }
}