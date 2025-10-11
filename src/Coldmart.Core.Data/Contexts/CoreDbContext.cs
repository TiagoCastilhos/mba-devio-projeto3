using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Core.Data.Contexts;

public class CoreDbContext : IdentityDbContext
{
    public CoreDbContext(DbContextOptions options)
        : base(options)
    {
    }
}
