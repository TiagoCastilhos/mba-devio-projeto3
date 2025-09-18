using Coldmart.Core;

namespace Coldmart.Alunos.Domain;

public class Aluno : Entity, IAggregateRoot
{
    public string Nome { get; protected set; }
    public string Email { get; protected set; }
    public List<Matricula>? Matriculas { get; protected set; }
    public List<Certificado>? Certificados { get; protected set; }

    public Aluno(string nome, string email)
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
