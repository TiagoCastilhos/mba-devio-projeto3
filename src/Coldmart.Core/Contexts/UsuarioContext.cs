using Microsoft.AspNetCore.Http;

namespace Coldmart.Core.Contexts;

public class UsuarioContext : IUsuarioContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid ObterIdUsuario()
    {
        // Implementation to retrieve the user ID from the current context
        throw new NotImplementedException();
    }
}