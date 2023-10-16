using FluentValidation;
using MVM.Estacionamento.Business.Models;
using MVM.Estacionamento.Business.Validations;

public class VeiculoValidation : AbstractValidator<Veiculo>
{
    public VeiculoValidation()
    {
        RuleFor(veiculo => veiculo.EmpresaId).NotEmpty().WithMessage("O campo EmpresaId é obrigatório");

        RuleFor(veiculo => veiculo.Marca)
            .NotEmpty().WithMessage("O campo Marca é obrigatório")
            .Length(2, 100).WithMessage("O campo Marca deve conter entre 2 e 100 caracteres");

        RuleFor(veiculo => veiculo.Modelo)
            .NotEmpty().WithMessage("O campo Modelo é obrigatório")
            .Length(2, 100).WithMessage("O campo Modelo deve conter entre 2 e 100 caracteres");

        RuleFor(veiculo => veiculo.Cor)
            .NotEmpty().WithMessage("O campo Cor é obrigatório")
            .Length(2, 50).WithMessage("O campo Cor deve conter entre 2 e 50 caracteres");

        RuleFor(veiculo => veiculo.Placa)
            .NotEmpty().WithMessage("O campo Placa é obrigatório")
            .Matches(@"^[A-Z]{3}\d{4}$").WithMessage("A placa deve seguir o padrão 'AAA1234'");

        RuleFor(veiculo => veiculo.Ano)
            .NotEmpty().WithMessage("O campo Ano é obrigatório")
            .InclusiveBetween(new DateTime(1900, 1, 1), new DateTime(2100, 12, 31))
            .WithMessage("O ano deve estar no intervalo de 1900 a 2100");

        RuleFor(veiculo => veiculo.Tipo).NotEmpty().WithMessage("O campo Tipo é obrigatório");

        RuleForEach(veiculo => veiculo.RegistrosEstacionamento)
            .SetValidator(new RegistrosEstacionamentoValidation());
    }
}
