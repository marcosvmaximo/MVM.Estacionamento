using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MVM.Estacionamento.Business.ValueObjects;

namespace MVM.Estacionamento.Api.ViewModels;

public readonly record struct EmpresaViewModel
{
    [Key]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid Id { get; init; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get; init; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(14, ErrorMessage = "O campo {0} deve conter {1} caracteres")]
    public string Cnpj { get; init; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public EnderecoViewModel Endereco { get; init; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public TelefoneViewModel Telefone { get; init; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int? QuantidadeVagasMotos { get; init; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int? QuantidadeVagasCarros { get; init; }
}

