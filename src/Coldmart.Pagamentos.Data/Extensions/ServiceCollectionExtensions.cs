using Coldmart.Pagamentos.Data.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Coldmart.Pagamentos.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPagamentosData(this IServiceCollection services)
    {
        services.AddDbContext<IPagamentosDbContext, PagamentosDbContext>();
        return services;
    }
}
