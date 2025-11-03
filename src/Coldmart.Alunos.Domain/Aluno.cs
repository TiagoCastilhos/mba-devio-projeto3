using Coldmart.Core.Domain;

namespace Coldmart.Alunos.Domain;

public class Aluno : Entity, IAggregateRoot
{
    public string Nome { get; protected set; }
    public string Email { get; protected set; }
    public virtual List<Matricula>? Matriculas { get; protected set; }
    public virtual List<Certificado>? Certificados { get; protected set; }
    public virtual List<HistoricoAluno>? Historicos { get; protected set; }

    public Aluno(Guid id, string nome, string email)
        : base(id)
    {
        ArgumentException.ThrowIfNullOrEmpty(nome, nameof(nome));
        ArgumentException.ThrowIfNullOrEmpty(email, nameof(email));

        Nome = nome;
        Email = email;
    }

    protected Aluno() { }

    public void AdicionarMatricula(Matricula matricula)
    {
        ArgumentNullException.ThrowIfNull(matricula, nameof(matricula));

        (Matriculas ??= []).Add(matricula);
    }

    public void AdicionarCertificado(Certificado certificado)
    {
        ArgumentNullException.ThrowIfNull(certificado, nameof(certificado));

        (Certificados ??= []).Add(certificado);
    }
}
