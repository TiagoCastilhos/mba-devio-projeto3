using System.ComponentModel.DataAnnotations;

namespace Coldmart.Alunos.Business.ViewModels;

public class RealizarAulaViewModel
{
    [Required]
    public Guid AulaId { get; set; }
}
