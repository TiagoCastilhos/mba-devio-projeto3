﻿using Coldmart.Core.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Coldmart.Core.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreData(this IServiceCollection services)
    {
        services
            .AddIdentityCore<IdentityUser>(ConfigureIdentityOptions)
            .AddEntityFrameworkStores<CoreDbContext>()
            .AddSignInManager();

        return services;
    }

    private static void ConfigureIdentityOptions(IdentityOptions options)
    {
        options.SignIn.RequireConfirmedAccount = false;

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
