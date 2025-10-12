using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Coldmart.Core.Contexts;

public class UsuarioContext : IUsuarioContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? ObterIdUsuario()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null)
            return null;

        var idClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (idClaim == null)
            return null;

        return Guid.Parse(idClaim);
    }
}