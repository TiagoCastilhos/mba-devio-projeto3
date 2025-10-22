using Coldmart.Pagamentos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Coldmart.Pagamentos.Data.Contexts;

public interface IPagamentosDbContext
{
    DbSet<Pagamento> Pagamentos { get; set; }
    DbSet<DadosCartao> DadosCartoes { get; set; }
    DbSet<Matricula> Matriculas { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DatabaseFacade Database { get; }
}