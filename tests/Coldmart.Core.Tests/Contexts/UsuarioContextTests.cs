using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Coldmart.Core.Contexts;
using Coldmart.Core.Tests.Attributes;
using Microsoft.AspNetCore.Http;

namespace Coldmart.Core.Tests.Contexts;

[ExcludeFromCodeCoverage]
public class UsuarioContextTests
{
    [Theory, AutoDomainData]
    public void ObterIdUsuario_QuandoUsuarioAutenticado_DeveRetornarIdUsuario(
        Guid idUsuario)
    {
        //arrange
        var httpContext = new DefaultHttpContext();
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, idUsuario.ToString())
        };

        var identity = new ClaimsIdentity(claims, "Bearer");
        httpContext.User = new ClaimsPrincipal(identity);

        var httpContextAccessor = new HttpContextAccessor
        {
            HttpContext = httpContext
        };

        var context = new UsuarioContext(httpContextAccessor);

        //act
        var actualUserId = context.ObterIdUsuario();

        //assert
        Assert.NotNull(actualUserId);
        Assert.Equal(idUsuario, actualUserId);
    }

    [Fact]
    public void ObterIdUsuario_QuandoUsuarioNaoAutenticado_DeveRetornarNulo()
    {
        //arrange
        var httpContext = new DefaultHttpContext();
        var httpContextAccessor = new HttpContextAccessor
        {
            HttpContext = httpContext
        };
        var context = new UsuarioContext(httpContextAccessor);

        //act
        var actualUserId = context.ObterIdUsuario();

        //assert
        Assert.Null(actualUserId);
    }
}
