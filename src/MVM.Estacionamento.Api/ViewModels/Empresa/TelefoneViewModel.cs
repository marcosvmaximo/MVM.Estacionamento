using System;
using System.ComponentModel.DataAnnotations;

namespace MVM.Estacionamento.Api.ViewModels;

public readonly record struct TelefoneViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(2, ErrorMessage = "O campo {0} deve conter {1} caracteres")]
    public string Ddd { get; init; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(9, ErrorMessage = "O campo {0} deve conter {1} caracteres")]
    public string TelefoneCelular { get; init; }

    //[StringLength(9, ErrorMessage = "O campo {0} deve conter {1} caracteres", MinimumLength = 2)]
    public string TelefoneFixo { get; init; }
}

