using Coldmart.Core;

namespace Coldmart.Alunos.Domain;

public class HistoricoAprendizado : Entity
{
    public Aluno Aluno { get; protected set; }
    public List<Certificado>? Certificados { get; protected set; }
    public List<Matricula>? Matriculas { get; set; }
}