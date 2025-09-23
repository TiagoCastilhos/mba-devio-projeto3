namespace Coldmart.Core;

public sealed class Notificador : INotificador
{
    private readonly List<string> _erros = [];

    public void AdicionarErro(string mensagem)
    {
        _erros.Add(mensagem);
    }

    public IReadOnlyList<string> ObterErros() => _erros.AsReadOnly();

    public bool TemErro() => _erros.Any();
}