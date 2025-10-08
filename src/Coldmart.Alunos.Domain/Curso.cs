namespace Coldmart.Alunos.Domain;

public class Curso
{
    public Guid Id { get; set; }
    public bool Deletado { get; set; }
    public List<Aula> Aulas { get; set; }

    public Curso() { }
}
