using Coldmart.Pagamentos.Business.ViewModels;
using Coldmart.Pagamentos.Data.Contexts;

namespace Coldmart.Pagamentos.Business.Services;

public class PagamentosService
{
    private readonly PagamentosDbContext _dbContext;

    public PagamentosService(PagamentosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CriarPagamentoAsync(PagamentoViewModel pagamento)
    {

    }
}
