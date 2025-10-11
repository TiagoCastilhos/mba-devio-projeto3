using Coldmart.Cursos.Data.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Coldmart.Cursos.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCursosData(this IServiceCollection services)
    {
        services.AddDbContext<ICursosDbContext, CursosDbContext>();
        return services;
    }
}
