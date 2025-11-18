using System.Diagnostics.CodeAnalysis;
using Coldmart.Core.Data.Contexts;
using Coldmart.Core.Data.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coldmart.Core.Data.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreData(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services
            .AddIdentityCore<IdentityUser>(ConfigureIdentityOptions)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<CoreDbContext>()
            .AddSignInManager();

        services.AddDbContext<ICoreDbContext, CoreDbContext>(options =>
        {
            options.ConfigureDbContextOptions(configuration, isDevelopment);
        });

        services.AddScoped<IDbSeeder, DbSeeder>();
        services.AddScoped<IDbContextSeeder, CoreDbContextSeeder>();

        return services;
    }

    private static void ConfigureIdentityOptions(IdentityOptions options)
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;

        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        options.User.RequireUniqueEmail = true;
    }
}
