using System.Diagnostics.CodeAnalysis;
using Coldmart.Core.Constants;
using Coldmart.Core.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Core.Data.Seeders;

[ExcludeFromCodeCoverage]
public class CoreDbContextSeeder : IDbContextSeeder
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ICoreDbContext _coreDbContext;

    public CoreDbContextSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ICoreDbContext coreDbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _coreDbContext = coreDbContext;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        if (await _coreDbContext.Users.AnyAsync(cancellationToken))
            return;

        await _roleManager.CreateAsync(new IdentityRole(RolesConstants.Admin));
        await _roleManager.CreateAsync(new IdentityRole(RolesConstants.Usuario));

        var usuarioAdmin = new IdentityUser("admin@coldmart.com")
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "admin@coldmart.com",
            Email = "admin@coldmart.com",
        };

        await _userManager.CreateAsync(usuarioAdmin, "Admin@123");
        await _userManager.AddToRoleAsync(usuarioAdmin, RolesConstants.Admin);

        var usuario = new IdentityUser("aluno@coldmart.com")
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "aluno@coldmart.com",
            Email = "aluno@coldmart.com",
        };

        await _userManager.CreateAsync(usuario, "Aluno@123");
        await _userManager.AddToRoleAsync(usuario, RolesConstants.Usuario);
    }
}
