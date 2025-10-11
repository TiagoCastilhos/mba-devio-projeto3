using Coldmart.Pagamentos.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Coldmart.Pagamentos.Business.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPagamentosBusiness(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PagamentosService).Assembly));
        return services;
    }
}
