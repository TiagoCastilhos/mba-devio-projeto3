using Coldmart.Core.Contexts;
using Coldmart.Core.Eventos;
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

        services.AddMediatR(cfg =>
        {
            cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzkwMzgwODAwIiwiaWF0IjoiMTc1ODkxNDQ2NiIsImFjY291bnRfaWQiOiIwMTk5ODc3ODZjZGU3MWIxYjFhODMyMDVhZDkyZjRmYyIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazYzcWhuMzIzNDFwbmMyMDV3MHl3OTg3Iiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.hjYvS596Jk0YMfEHjVtAko70pxTrPo4-CRoJPnyYESjo6p0fkLpUJILMFyyONcTSAiWYZ5p_cpsgcpWQ-esvZYzFDH-GU1MMiozYHHZo4XyFma3QNfbqUURmeTh_WTb_6RkZsGWI3NDxz6_L3pjVfMoTzWffSElvzGOKF_1DoKJ4lWzN0DB9JzVH59BmE3qIF5IcuKHu0w9OEpEmEgnnCXOOhb8yFJBhbD4RNpqGrDj1tmbUcFMMIza_ldhJ2ect8aO6NkwYvpdTQ_p02SkJjEP5o3UnvMmIWWekFHEFN08EgY9IpjigoNJw-Ot0lVsraAwldkRWLi7bfcSmcomHYw";
            cfg.RegisterServicesFromAssemblyContaining<MatriculaRealizadaEvento>();
        });

        services.AddScoped<IAutenticacaoService, AutenticacaoService>();

        return services;
    }
}
