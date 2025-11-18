using System.Diagnostics.CodeAnalysis;
using Coldmart.Pagamentos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Pagamentos.Data.Contexts;

[ExcludeFromCodeCoverage]
public class PagamentosDbContext : DbContext, IPagamentosDbContext
{
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<DadosCartao> DadosCartoes { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }

    public PagamentosDbContext(DbContextOptions<PagamentosDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentosDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
