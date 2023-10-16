using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MVM.Estacionamento.Business.ValueObjects;

namespace MVM.Estacionamento.Api.ViewModels;

public class EmpresaViewModel
{
    [Key]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(14, ErrorMessage = "O campo {0} deve conter {1} caracteres")]
    public string Cnpj { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public EnderecoViewModel Endereco { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public TelefoneViewModel Telefone { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int? QuantidadeVagasMotos { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int? QuantidadeVagasCarros { get; set; }
}

