using Coldmart.Core;

namespace Coldmart.Alunos.Domain;

public class HistoricoAprendizado : Entity
{
    public required Aluno Aluno { get; set; }
    public List<Certificado>? Certificados { get; set; }
    public List<Matricula>? Matriculas { get; set; }
}