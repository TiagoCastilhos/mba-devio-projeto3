using Coldmart.Core.Notificacao;
using Coldmart.Core.Services;
using Coldmart.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Coldmart.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : CustomControllerBase
{
    private readonly IAutenticacaoService _autenticacaoService;

    public AuthController(IAutenticacaoService autenticacaoService, INotificador notificador) 
        : base(notificador)
    {
        _autenticacaoService = autenticacaoService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Logar([FromBody] LogarViewModel viewModel)
    {
        var token = await _autenticacaoService.GerarTokenAsync(viewModel);
        return CustomResponse(new { AccessToken = token });
    }
}
