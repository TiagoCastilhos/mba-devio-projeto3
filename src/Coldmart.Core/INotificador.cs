namespace Coldmart.Core;

public interface INotificador
{
    bool TemErro();
    IReadOnlyList<string> ObterErros();
    void AdicionarErro(string mensagem);
}
