using Coldmart.Alunos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Coldmart.Alunos.Data.Contexts;

public interface IAlunosDbContext
{
    DbSet<Aluno> Alunos { get; }
    DbSet<Matricula> Matriculas { get; }
    DbSet<Certificado> Certificados { get; }
    DbSet<Curso> Cursos { get; }
    DbSet<Aula> Aulas { get; }
    DbSet<HistoricoAluno> HistoricosAlunos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DatabaseFacade Database { get; }
}