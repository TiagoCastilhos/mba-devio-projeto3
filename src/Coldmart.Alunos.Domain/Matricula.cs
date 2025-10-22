using Coldmart.Alunos.Domain.Enumerations;
using Coldmart.Core.Domain;

namespace Coldmart.Alunos.Domain;

public class Matricula : Entity
{
    public Guid CursoId { get; protected set; }
    public virtual Curso Curso { get; protected set; }
    public Guid AlunoId { get; protected set; }
    public virtual Aluno Aluno { get; protected set; }
    public StatusMatricula Status { get; protected set; }
    public DateTimeOffset DataAtualizacao { get; protected set; }

    public Matricula(Curso curso, Aluno aluno)
        : base(Guid.NewGuid())
    {
        ArgumentNullException.ThrowIfNull(curso, nameof(curso));
        ArgumentNullException.ThrowIfNull(aluno, nameof(aluno));

        Curso = curso;
        CursoId = curso.Id;
        Aluno = aluno;
        AlunoId = aluno.Id;
        Status = StatusMatricula.AguardandoPagamento;
        DataAtualizacao = DateTimeOffset.UtcNow;
    }

    protected Matricula() { }

    public void Iniciar()
    {
        Status = StatusMatricula.Iniciado;
        DataAtualizacao = DateTimeOffset.UtcNow;
    }

    public void Concluir()
    {
        Status = StatusMatricula.Concluido;
        DataAtualizacao = DateTimeOffset.UtcNow;
    }

    public void Cancelar()
    {
        Status = StatusMatricula.Cancelado;
        DataAtualizacao = DateTimeOffset.UtcNow;
    }
}
