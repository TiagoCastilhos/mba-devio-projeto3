using Coldmart.Cursos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Cursos.Data.Contexts;

public interface ICursosDbContext
{
    DbSet<Curso> Cursos { get; }
    DbSet<Aula> Aulas { get; }
    DbSet<ConteudoProgramatico> ConteudosProgramaticos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}