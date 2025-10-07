using System.ComponentModel.DataAnnotations;

namespace Coldmart.Pagamentos.Business.ViewModels;

public class AlterarStatusPagamentoViewModel
{
    [Required]
    public Guid PagamentoId { get; set; }
}