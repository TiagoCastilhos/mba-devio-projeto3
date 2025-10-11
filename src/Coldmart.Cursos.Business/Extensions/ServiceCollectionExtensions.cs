using Coldmart.Cursos.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Coldmart.Cursos.Business.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCursosBusiness(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CursosService).Assembly));
        return services;
    }
}
