using System.Diagnostics.CodeAnalysis;
using Coldmart.Alunos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Alunos.Data.Contexts;

[ExcludeFromCodeCoverage]
public sealed class AlunosDbContext : DbContext, IAlunosDbContext
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Certificado> Certificados { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Aula> Aulas { get; set; }
    public DbSet<HistoricoAluno> HistoricosAlunos { get; set; }

    public AlunosDbContext(DbContextOptions<AlunosDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AlunosDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
