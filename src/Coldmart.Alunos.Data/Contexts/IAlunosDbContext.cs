using Coldmart.Alunos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Alunos.Data.Contexts;

public interface IAlunosDbContext
{
    DbSet<Aluno> Alunos { get; }
    DbSet<Matricula> Matriculas { get; }
    DbSet<Certificado> Certificados { get; }
    DbSet<Curso> Cursos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}