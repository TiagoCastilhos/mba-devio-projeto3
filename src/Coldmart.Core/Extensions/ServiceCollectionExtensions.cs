using Coldmart.Core.Contexts;
using Coldmart.Core.Notificacao;
using Coldmart.Core.Options;
using Coldmart.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coldmart.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        var identityOptions = new JwtOptions();
        configuration.GetSection(JwtOptions.SectionName).Bind(identityOptions);

        services.AddSingleton(identityOptions);

        services.AddScoped<IUsuarioContext, UsuarioContext>();
        services.AddScoped<INotificador, Notificador>();

        services.AddScoped<IAutenticacaoService, AutenticacaoService>();

        return services;
    }
}
