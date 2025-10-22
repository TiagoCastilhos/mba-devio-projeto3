using Coldmart.Core.Domain;

namespace Coldmart.Cursos.Domain;

public class Curso : Entity, IAggregateRoot
{
    public string Nome { get; protected set; }
    public TimeSpan DuracaoTotal { get; protected set; }
    public virtual List<ConteudoProgramatico>? ConteudosProgramaticos { get; protected set; }
    public virtual List<Aula>? Aulas { get; protected set; }

    protected Curso() { }

    public Curso(string nome)
        : base(Guid.NewGuid())
    {
        ArgumentException.ThrowIfNullOrEmpty(nome, nameof(nome));

        Nome = nome;
        DuracaoTotal = TimeSpan.Zero;
    }

    public void AdicionarConteudoProgramatico(ConteudoProgramatico conteudoProgramatico)
    {
        ArgumentNullException.ThrowIfNull(conteudoProgramatico, nameof(conteudoProgramatico));

        (ConteudosProgramaticos ??= []).Add(conteudoProgramatico);
    }

    public void AdicionarAula(Aula aula)
    {
        ArgumentNullException.ThrowIfNull(aula);

        (Aulas ??= []).Add(aula);
        DuracaoTotal += aula.Duracao;
    }
}
