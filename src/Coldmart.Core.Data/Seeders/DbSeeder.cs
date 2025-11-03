using Microsoft.Extensions.DependencyInjection;

namespace Coldmart.Core.Data.Seeders;

public class DbSeeder : IDbSeeder
{
    private readonly IServiceProvider _serviceProvider;

    public DbSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var seeders = scope.ServiceProvider.GetServices<IDbContextSeeder>();

        if (!seeders.Any())
            return;

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(cancellationToken);
        }
    }
}