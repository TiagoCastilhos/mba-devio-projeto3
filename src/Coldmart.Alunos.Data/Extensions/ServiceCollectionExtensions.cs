using Coldmart.Alunos.Data.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Coldmart.Alunos.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAlunosData(this IServiceCollection services)
    {
        services.AddDbContext<IAlunosDbContext, AlunosDbContext>();
        return services;
    }
}
