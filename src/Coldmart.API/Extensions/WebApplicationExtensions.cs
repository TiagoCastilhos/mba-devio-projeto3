using Coldmart.Alunos.Data.Contexts;
using Coldmart.Core.Data.Contexts;
using Coldmart.Cursos.Data.Contexts;
using Coldmart.Pagamentos.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.API.Extensions;

public static class WebApplicationExtensions
{
    public static async Task<WebApplication> AplicarMigracoesAsync(this WebApplication app, IWebHostEnvironment environment)
    {
        using var scope = app.Services.CreateScope();

        var alunosDbContext = scope.ServiceProvider.GetRequiredService<IAlunosDbContext>();
        await alunosDbContext.Database.MigrateAsync(CancellationToken.None);

        var cursosDbContext = scope.ServiceProvider.GetRequiredService<ICursosDbContext>();
        await cursosDbContext.Database.MigrateAsync(CancellationToken.None);

        var pagamentosDbContext = scope.ServiceProvider.GetRequiredService<IPagamentosDbContext>();
        await pagamentosDbContext.Database.MigrateAsync(CancellationToken.None);

        var coreDbContext = scope.ServiceProvider.GetRequiredService<ICoreDbContext>();
        await coreDbContext.Database.MigrateAsync(CancellationToken.None);

        return app;
    }
}