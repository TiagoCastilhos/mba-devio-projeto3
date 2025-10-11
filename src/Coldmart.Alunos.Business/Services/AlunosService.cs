using Coldmart.Alunos.Business.Requests;
using Coldmart.Alunos.Data.Contexts;
using Coldmart.Alunos.Domain;
using Coldmart.Core.Contexts;
using Coldmart.Core.Eventos;
using Coldmart.Core.Notificacao;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Alunos.Business.Services;

public class AlunosService : IRequestHandler<MatricularAoCursoRequest>, IRequestHandler<RealizarAulaRequest>
{
    private readonly IAlunosDbContext _dbContext;
    private readonly IUsuarioContext _usuarioContext;
    private readonly INotificador _notificador;
    private readonly IMediator _mediator;

    public AlunosService(IAlunosDbContext dbContext, IUsuarioContext usuarioContext, INotificador notificador, IMediator mediator)
    {
        _dbContext = dbContext;
        _usuarioContext = usuarioContext;
        _notificador = notificador;
        _mediator = mediator;
    }

    public async Task Handle(MatricularAoCursoRequest request, CancellationToken cancellationToken)
    {
        var alunoId = _usuarioContext.ObterIdUsuario();

        var aluno = await _dbContext.Alunos.FirstOrDefaultAsync(a => a.Id == alunoId, cancellationToken);
        if (aluno == null)
        {
            _notificador.AdicionarErro($"Aluno '{alunoId}' não encontrado.");
            return;
        }

        var curso = await _dbContext.Cursos.FirstOrDefaultAsync(c => c.Id == request.Matricula.CursoId, cancellationToken);
        if (curso == null)
        {
            _notificador.AdicionarErro($"Curso '{request.Matricula.CursoId}' não encontrado.");
            return;
        }

        var matricula = new Matricula(curso, aluno);
        await _dbContext.Matriculas.AddAsync(matricula, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new MatriculaRealizadaEvento { AlunoId = aluno.Id, CursoId = curso.Id }, cancellationToken);
    }

    public async Task Handle(RealizarAulaRequest request, CancellationToken cancellationToken)
    {
        var alunoId = _usuarioContext.ObterIdUsuario();

        var aluno = await _dbContext.Alunos.FirstOrDefaultAsync(a => a.Id == alunoId, cancellationToken);
        if (aluno == null)
        {
            _notificador.AdicionarErro($"Aluno '{alunoId}' não encontrado.");
            return;
        }

        var aula = await _dbContext.Aulas.FirstOrDefaultAsync(a => a.Id == request.Aula.AulaId, cancellationToken);
        if (aula == null)
        {
            _notificador.AdicionarErro($"Aula '{request.Aula.AulaId}' não encontrada.");
            return;
        }

        var historicoAluno = new HistoricoAluno(aluno.Id, aula.Id, aula.CursoId);
        await _dbContext.HistoricosAlunos.AddAsync(historicoAluno, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new AulaRealizadaEvento { AlunoId = aluno.Id, AulaId = aula.Id, CursoId = aula.CursoId }, cancellationToken);
    }
}
