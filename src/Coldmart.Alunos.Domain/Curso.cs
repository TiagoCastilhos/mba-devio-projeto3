namespace Coldmart.Alunos.Domain;

public class Curso
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public bool Deletado { get; set; }

    public Curso() { }
}