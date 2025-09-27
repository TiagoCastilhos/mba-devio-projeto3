using Coldmart.Core.Domain;

namespace Coldmart.Cursos.Domain;

public class ConteudoProgramatico : Entity
{
    public Guid CursoId { get; protected set; }
    public string Titulo { get; protected set; }
    public string Descricao { get; protected set; }

    protected ConteudoProgramatico() { }

    public ConteudoProgramatico(Curso curso, string titulo, string descricao)
    {
        ArgumentNullException.ThrowIfNull(curso, nameof(curso));
        ArgumentException.ThrowIfNullOrEmpty(titulo, nameof(titulo));
        ArgumentException.ThrowIfNullOrEmpty(descricao, nameof(descricao));

        CursoId = curso.Id;
        Titulo = titulo;
        Descricao = descricao;
    }
}