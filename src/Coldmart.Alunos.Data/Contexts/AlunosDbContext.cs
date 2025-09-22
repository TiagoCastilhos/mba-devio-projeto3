using Coldmart.Alunos.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Alunos.Data.Contexts;

public sealed class AlunosDbContext : IdentityDbContext
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Certificado> Certificados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AlunosDbContext).Assembly);

        //SeedDatabase.Seed(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}
