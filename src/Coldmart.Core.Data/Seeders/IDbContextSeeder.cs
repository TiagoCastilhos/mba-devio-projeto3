namespace Coldmart.Core.Data.Seeders;

public interface IDbContextSeeder
{
    Task SeedAsync(CancellationToken cancellationToken);
}