using Coldmart.Alunos.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Coldmart.Alunos.Business.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAlunosBusiness(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AlunosService).Assembly));
        return services;
    }
}
