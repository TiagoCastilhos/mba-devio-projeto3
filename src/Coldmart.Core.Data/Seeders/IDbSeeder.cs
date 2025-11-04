namespace Coldmart.Core.Data.Seeders;

public interface IDbSeeder
{
    Task SeedAsync(CancellationToken cancellationToken);
}
