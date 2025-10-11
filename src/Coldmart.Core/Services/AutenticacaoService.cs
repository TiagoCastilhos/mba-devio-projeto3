using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Coldmart.Core.Notificacao;
using Coldmart.Core.Options;
using Coldmart.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Coldmart.Core.Services;

public class AutenticacaoService : IAutenticacaoService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly JwtOptions _jwtOptions;
    private readonly INotificador _notificador;

    public AutenticacaoService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, JwtOptions jwtOptions, INotificador notificador)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtOptions = jwtOptions;
        _notificador = notificador;
    }

    public async Task<string?> GerarTokenAsync(LogarViewModel inputModel)
    {
        var user = await _userManager.FindByEmailAsync(inputModel.Email);

        if (user == null)
        {
            _notificador.AdicionarErro("Não foi possível autenticar o usuário. Verifique as credenciais e tente novamente.");
            return null;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, inputModel.Senha, false);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                _notificador.AdicionarErro("Usuário está bloqueado devido ao excesso de tentativas.");
                return null;
            }

            _notificador.AdicionarErro("Não foi possível autenticar o usuário. Verifique as credenciais e tente novamente.");
            return null;
        }

        return GerarTokenJwt(user);
    }

    private string GerarTokenJwt(IdentityUser user)
    {
        var key = Encoding.ASCII.GetBytes(_jwtOptions.SigningKey);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),
                new(JwtRegisteredClaimNames.Aud, _jwtOptions.Audience),
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Role, "Aluno"), //Verificar as roles
                new(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
                new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64),
            ]),
            Expires = DateTime.UtcNow.AddHours(_jwtOptions.TokenExpirationInHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        return jwtSecurityTokenHandler.WriteToken(token);
    }

    private static long ToUnixEpochDate(DateTime date)
    => (long)Math.Round((date.ToUniversalTime() - DateTime.UnixEpoch).TotalSeconds);
}
