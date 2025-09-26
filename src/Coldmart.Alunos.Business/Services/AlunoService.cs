using Coldmart.Alunos.Business.ViewModels;
using Coldmart.Alunos.Data.Contexts;
using Coldmart.Alunos.Domain;
using Coldmart.Core.Contexts;
using Coldmart.Core.Notificacao;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Alunos.Business.Services;

public class AlunoService
{
    private readonly IAlunosDbContext _dbContext;
    private readonly IUsuarioContext _usuarioContext;
    private readonly INotificador _notificador;

    public AlunoService(IAlunosDbContext dbContext, IUsuarioContext usuarioContext, INotificador notificador)
    {
        _dbContext = dbContext;
        _usuarioContext = usuarioContext;
        _notificador = notificador;
    }

    public async Task MatricularAoCursoAsync(MatriculaViewModel viewModel, CancellationToken cancellationToken)
    {
        var alunoId = _usuarioContext.ObterIdUsuario();

        var aluno = await _dbContext.Alunos.FirstOrDefaultAsync(a => a.Id == alunoId, cancellationToken);
        if (aluno == null)
        {
            _notificador.AdicionarErro($"Aluno '{alunoId}' não encontrado.");
            return;
        }

        var curso = await _dbContext.Cursos.FirstOrDefaultAsync(c => c.Id == viewModel.CursoId, cancellationToken);
        if (curso == null)
        {
            _notificador.AdicionarErro($"Curso '{viewModel.CursoId}' não encontrado.");
            return;
        }

        var matricula = new Matricula(curso, aluno);
        await _dbContext.Matriculas.AddAsync(matricula, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
