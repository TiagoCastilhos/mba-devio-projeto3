using Coldmart.Cursos.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Cursos.Data.Contexts;

public class CursosDbContext : IdentityDbContext, ICursosDbContext
{
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Aula> Aulas { get; set; }
    public DbSet<ConteudoProgramatico> ConteudosProgramaticos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CursosDbContext).Assembly);

        //SeedDatabase.Seed(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}
