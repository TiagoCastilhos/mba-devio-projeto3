namespace Coldmart.Alunos.Domain;

public class Curso
{
    public Guid Id { get; protected set; }
    public string Nome { get; protected set; }

    public Curso(Guid id, string nome)
    {
        ArgumentException.ThrowIfNullOrEmpty(nome, nameof(nome));
        ArgumentOutOfRangeException.ThrowIfEqual(id, Guid.Empty, nameof(id));

        Id = id;
        Nome = nome;
    }

    protected Curso() { }
}