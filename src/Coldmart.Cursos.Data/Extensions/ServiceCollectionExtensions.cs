﻿using Coldmart.Core.Data.Extensions;
using Coldmart.Cursos.Data.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coldmart.Cursos.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCursosData(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddDbContext<ICursosDbContext, CursosDbContext>(options =>
        {
            options.ConfigureDbContextOptions(configuration, isDevelopment);
        });

        return services;
    }
}
