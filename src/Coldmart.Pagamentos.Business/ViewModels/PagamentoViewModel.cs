﻿using System.ComponentModel.DataAnnotations;

namespace Coldmart.Pagamentos.Business.ViewModels;

public class PagamentoViewModel
{
    [Required]
    public DadosCartaoViewModel Cartao { get; set; }

    [Required]
    public Guid MatriculaId { get; set; }

    [Required]
    [MinLength(0)]
    public decimal Valor { get; set; }
}
