using Coldmart.Alunos.Data.Contexts;
using Coldmart.Alunos.Domain;
using Coldmart.Core.Eventos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Alunos.Business.Services;

public class AlunosEventosService : INotificationHandler<AulaRealizadaEvento>
{
    private readonly IAlunosDbContext _alunosDbContext;

    public AlunosEventosService(IAlunosDbContext alunosDbContext)
    {
        _alunosDbContext = alunosDbContext;
    }

    public async Task Handle(AulaRealizadaEvento notification, CancellationToken cancellationToken)
    {
        var aulasCurso = await _alunosDbContext.Aulas
            .Where(a => a.CursoId == notification.CursoId)
            .ToListAsync(cancellationToken);

        var aulasRealizadas = await _alunosDbContext.HistoricosAlunos
            .Where(h => h.AlunoId == notification.AlunoId && h.CursoId == notification.CursoId)
            .ToListAsync(cancellationToken);

        if (aulasCurso.Count != aulasRealizadas.Count)
            return;

        var curso = await _alunosDbContext.Cursos.FirstAsync(c => c.Id == notification.CursoId, cancellationToken);
        var aluno = await _alunosDbContext.Alunos.FirstAsync(a => a.Id == notification.AlunoId, cancellationToken);

        var certificado = new Certificado(curso, aluno);
        await _alunosDbContext.Certificados.AddAsync(certificado, cancellationToken);
        await _alunosDbContext.SaveChangesAsync(cancellationToken);
    }
}
