using System.ComponentModel.DataAnnotations;

namespace Coldmart.Pagamentos.Business.ViewModels;

public class PagamentoViewModel
{
    [Required]
    public DadosCartaoViewModel Cartao { get; set; }

    [Required]
    public Guid MatriculaId { get; set; }

    [Required]
    [Range(0, 99999)]
    public decimal Valor { get; set; }
}
