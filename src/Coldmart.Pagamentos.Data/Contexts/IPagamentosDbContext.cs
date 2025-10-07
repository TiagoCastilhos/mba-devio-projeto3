using Coldmart.Pagamentos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Pagamentos.Data.Contexts;

public interface IPagamentosDbContext
{
    DbSet<Pagamento> Pagamentos { get; set; }
    DbSet<DadosCartao> DadosCartoes { get; set; }
    DbSet<Matricula> Matriculas { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}