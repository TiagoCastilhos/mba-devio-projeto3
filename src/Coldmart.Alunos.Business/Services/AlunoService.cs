using Coldmart.Alunos.Business.ViewModels;
using Coldmart.Alunos.Data.Contexts;
using Coldmart.Alunos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Alunos.Business.Services;

public class AlunoService
{
    private readonly IAlunosDbContext _dbContext;

    public AlunoService(IAlunosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task MatricularAoCursoAsync(MatriculaViewModel viewModel, CancellationToken cancellationToken)
    {
        var alunoId = Guid.NewGuid(); //Get from http context

        var aluno = await _dbContext.Alunos.FirstOrDefaultAsync(a => a.Id == alunoId, cancellationToken);
        // throw if aluno is null

        var curso = await _dbContext.Cursos.FirstOrDefaultAsync(c => c.Id == viewModel.CursoId, cancellationToken);
        // throw if curso is null

        var matricula = new Matricula(curso, aluno);
        await _dbContext.Matriculas.AddAsync(matricula, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
