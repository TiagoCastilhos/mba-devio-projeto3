using Coldmart.Alunos.Data.Contexts;
using Coldmart.Core.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coldmart.Alunos.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAlunosData(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddDbContext<IAlunosDbContext, AlunosDbContext>(options =>
        {
            options.ConfigureDbContextOptions(configuration, isDevelopment);
        });

        return services;
    }
}
