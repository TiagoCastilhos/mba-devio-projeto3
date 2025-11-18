using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Coldmart.Core.Data.Extensions;

[ExcludeFromCodeCoverage]
public static class DbContextOptionsExtensions
{
    public static void ConfigureDbContextOptions(this DbContextOptionsBuilder options, IConfiguration configuration, bool isDevelopment)
    {
        // O ideal não seria usar lazy loading para todos os contextos da aplicação, apenas onde é necessário.
        // Decidi acidionar em todos para simplificar o desenvolvimento neste projeto.
        options.UseLazyLoadingProxies();

        if (isDevelopment)
            options.UseSqlite(configuration.GetConnectionString("ColdmartDb"));
        else
            options.UseSqlServer(configuration.GetConnectionString("ColdmartDb"));
    }
}
