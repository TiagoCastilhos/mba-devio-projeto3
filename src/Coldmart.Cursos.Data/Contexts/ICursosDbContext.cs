using Coldmart.Cursos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Coldmart.Cursos.Data.Contexts;

public interface ICursosDbContext
{
    DbSet<Curso> Cursos { get; }
    DbSet<Aula> Aulas { get; }
    DbSet<ConteudoProgramatico> ConteudosProgramaticos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DatabaseFacade Database { get; }
}