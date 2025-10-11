using Coldmart.Pagamentos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Pagamentos.Data.Contexts;

public class PagamentosDbContext : DbContext, IPagamentosDbContext
{
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<DadosCartao> DadosCartoes { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentosDbContext).Assembly);

        //SeedDatabase.Seed(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}
