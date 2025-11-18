using System.Diagnostics.CodeAnalysis;
using Coldmart.Cursos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Cursos.Data.Contexts;

[ExcludeFromCodeCoverage]
public class CursosDbContext : DbContext, ICursosDbContext
{
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Aula> Aulas { get; set; }
    public DbSet<ConteudoProgramatico> ConteudosProgramaticos { get; set; }

    public CursosDbContext(DbContextOptions<CursosDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CursosDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
