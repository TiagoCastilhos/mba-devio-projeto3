using Coldmart.Core;

namespace Coldmart.Alunos.Domain;

public class Certificado : Entity
{
    public required Aluno Aluno { get; set; }
}
