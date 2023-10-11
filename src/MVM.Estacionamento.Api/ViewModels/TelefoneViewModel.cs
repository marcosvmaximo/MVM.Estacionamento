using System;
using System.ComponentModel.DataAnnotations;

namespace MVM.Estacionamento.Api.ViewModels;

public class TelefoneViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(2, ErrorMessage = "O campo {0} deve conter {1} caracteres")]
    public string Ddd { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(9, ErrorMessage = "O campo {0} deve conter {1} caracteres")]
    public string TelefoneCelular { get; set; }

    [StringLength(9, ErrorMessage = "O campo {0} deve conter {1} caracteres", MinimumLength = 2)]
    public string TelefoneFixo { get; set; }
}

