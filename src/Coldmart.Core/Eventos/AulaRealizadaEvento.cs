using MediatR;

namespace Coldmart.Core.Eventos;

public class AulaRealizadaEvento : INotification
{
    public Guid AlunoId { get; set; }
    public Guid AulaId { get; set; }
    public Guid CursoId { get; set; }
}