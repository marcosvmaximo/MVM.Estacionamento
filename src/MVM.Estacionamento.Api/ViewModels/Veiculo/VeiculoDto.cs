using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MVM.Estacionamento.Business.Enum;

namespace MVM.Estacionamento.Api.ViewModels.Veiculo;

public class VeiculoDto
{
    [Key]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid EmpresaId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Marca { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Modelo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Cor { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [RegularExpression(@"^[A-Z]{3}\d{4}$", ErrorMessage = "A placa deve seguir o padrão 'AAA1234'")]
    public string Placa { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public DateTime Ano { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public ETipoVeiculo Tipo { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Status { get; set; }
}
