using Coldmart.Alunos.Business.ViewModels;
using Coldmart.Alunos.Data.Contexts;
using Coldmart.Alunos.Domain;
using Coldmart.Core;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Alunos.Business.Services;

public class AlunoService
{
    private readonly IAlunosDbContext _dbContext;
    private readonly IUsuarioContext _usuarioContext;

    public AlunoService(IAlunosDbContext dbContext, IUsuarioContext usuarioContext)
    {
        _dbContext = dbContext;
        _usuarioContext = usuarioContext;
    }

    public async Task MatricularAoCursoAsync(MatriculaViewModel viewModel, CancellationToken cancellationToken)
    {
        var alunoId = _usuarioContext.ObterIdUsuario();

        var aluno = await _dbContext.Alunos.FirstOrDefaultAsync(a => a.Id == alunoId, cancellationToken);
        // throw if aluno is null

        var curso = await _dbContext.Cursos.FirstOrDefaultAsync(c => c.Id == viewModel.CursoId, cancellationToken);
        // throw if curso is null

        var matricula = new Matricula(curso, aluno);
        await _dbContext.Matriculas.AddAsync(matricula, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
