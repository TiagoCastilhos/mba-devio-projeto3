using Coldmart.Core.Domain;

namespace Coldmart.Alunos.Domain;

public class Certificado : Entity
{
    public Guid CursoId { get; protected set; }
    public virtual Curso Curso { get; protected set; }
    public Guid AlunoId { get; protected set; }
    public virtual Aluno Aluno { get; protected set; }

    public Certificado(Curso curso, Aluno aluno)
        : base(Guid.NewGuid())
    {
        ArgumentNullException.ThrowIfNull(curso, nameof(curso));
        ArgumentNullException.ThrowIfNull(aluno, nameof(aluno));

        Curso = curso;
        CursoId = curso.Id;
        Aluno = aluno;
        AlunoId = aluno.Id;
    }

    protected Certificado() { }
}
