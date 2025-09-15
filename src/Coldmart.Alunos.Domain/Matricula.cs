using Coldmart.Alunos.Domain.Enumerations;
using Coldmart.Core;

namespace Coldmart.Alunos.Domain;

public class Matricula : Entity
{
    //public Curso { get; set; }
    public StatusMatricula Status { get; set; }
    public DateTimeOffset DataAtualizacao { get; set; }
}
