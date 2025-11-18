using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Core.Data.Contexts;

[ExcludeFromCodeCoverage]
public class CoreDbContext : IdentityDbContext, ICoreDbContext
{
    public CoreDbContext(DbContextOptions<CoreDbContext> options)
        : base(options)
    {
    }
}
