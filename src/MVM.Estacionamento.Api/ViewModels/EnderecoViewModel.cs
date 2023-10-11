using System;
using System.ComponentModel.DataAnnotations;

namespace MVM.Estacionamento.Api.ViewModels;

public class EnderecoViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(9, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres")]
    public string Cep { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Complemento { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int Numero { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Cidade { get; set; }
}

