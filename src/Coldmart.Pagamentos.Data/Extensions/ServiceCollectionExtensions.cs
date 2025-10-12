using Coldmart.Core.Data.Extensions;
using Coldmart.Pagamentos.Data.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coldmart.Pagamentos.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPagamentosData(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddDbContext<IPagamentosDbContext, PagamentosDbContext>(options =>
        {
            options.ConfigureDbContextOptions(configuration, isDevelopment);
        });

        return services;
    }
}
