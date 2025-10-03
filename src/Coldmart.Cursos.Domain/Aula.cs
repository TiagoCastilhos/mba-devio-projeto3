using Coldmart.Core.Domain;

namespace Coldmart.Cursos.Domain;

public class Aula : Entity
{
    public Guid CursoId { get; protected set; }
    public string Titulo { get; protected set; }
    public TimeSpan Duracao { get; protected set; }

    protected Aula() { }

    public Aula(Curso curso, string titulo, TimeSpan duracao)
    {
        ArgumentNullException.ThrowIfNull(curso, nameof(curso));
        ArgumentException.ThrowIfNullOrEmpty(titulo, nameof(titulo));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(duracao.TotalSeconds, nameof(duracao));

        CursoId = curso.Id;
        Titulo = titulo;
        Duracao = duracao;
    }
}
