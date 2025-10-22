using Coldmart.Core.Domain;

namespace Coldmart.Alunos.Domain;

public class HistoricoAluno : Entity
{
    public Guid AlunoId { get; protected set; }
    public Guid AulaId { get; protected set; }
    public Guid CursoId { get; protected set; }

    public HistoricoAluno(Guid alunoId, Guid aulaId, Guid cursoId)
        : base(Guid.NewGuid())
    {
        ArgumentOutOfRangeException.ThrowIfEqual(alunoId, Guid.Empty, nameof(alunoId));
        ArgumentOutOfRangeException.ThrowIfEqual(aulaId, Guid.Empty, nameof(aulaId));
        ArgumentOutOfRangeException.ThrowIfEqual(cursoId, Guid.Empty, nameof(cursoId));

        AlunoId = alunoId;
        AulaId = aulaId;
        CursoId = cursoId;
    }

    protected HistoricoAluno() { }
}
