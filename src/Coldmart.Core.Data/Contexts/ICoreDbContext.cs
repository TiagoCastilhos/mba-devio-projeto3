using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Coldmart.Core.Data.Contexts;

public interface ICoreDbContext
{
    DbSet<IdentityUser> Users { get; }
    DbSet<IdentityRole> Roles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DatabaseFacade Database { get; }
}