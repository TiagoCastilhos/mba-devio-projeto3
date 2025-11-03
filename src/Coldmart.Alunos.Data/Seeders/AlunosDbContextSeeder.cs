using Coldmart.Alunos.Data.Contexts;
using Coldmart.Alunos.Domain;
using Coldmart.Core.Data.Contexts;
using Coldmart.Core.Data.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Alunos.Data.Seeders;

internal class AlunosDbContextSeeder : IDbContextSeeder
{
    private readonly IAlunosDbContext _alunosDbContext;
    private readonly ICoreDbContext _coreDbContext;

    public AlunosDbContextSeeder(IAlunosDbContext alunosDbContext, ICoreDbContext coreDbContext)
    {
        _alunosDbContext = alunosDbContext;
        _coreDbContext = coreDbContext;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        if (await _alunosDbContext.Alunos.AnyAsync(cancellationToken))
            return;

        var usuarioAluno = await _coreDbContext.Users.FirstAsync(u => u.Email == "aluno@colmart.com", cancellationToken);

        var aluno = new Aluno(Guid.Parse(usuarioAluno.Id), usuarioAluno.UserName!, usuarioAluno.Email!);
        await _alunosDbContext.Alunos.AddAsync(aluno, cancellationToken);

        await _alunosDbContext.SaveChangesAsync(cancellationToken);
    }
}
