using Coldmart.Core;

namespace Coldmart.Alunos.Domain;

public class Aluno : Entity, IAggregateRoot
{
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public List<Matricula>? Matriculas { get; set; }
}
