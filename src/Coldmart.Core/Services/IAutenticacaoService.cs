using Coldmart.Core.ViewModels;

namespace Coldmart.Core.Services;

public interface IAutenticacaoService
{
    Task<string?> GerarTokenAsync(LogarViewModel inputModel);
}
