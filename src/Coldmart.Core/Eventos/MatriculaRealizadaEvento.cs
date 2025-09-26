﻿using MediatR;

namespace Coldmart.Core.Eventos;

public class MatriculaRealizadaEvento : INotification
{
    public Guid AlunoId { get; init; }
    public Guid CursoId { get; init; }
}
