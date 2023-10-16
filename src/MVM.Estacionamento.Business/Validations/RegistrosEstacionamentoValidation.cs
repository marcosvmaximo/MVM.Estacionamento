using System;
using FluentValidation;
using MVM.Estacionamento.Business.Models;

namespace MVM.Estacionamento.Business.Validations;

public class RegistrosEstacionamentoValidation : AbstractValidator<RegistroEstacionamento>
{
    public RegistrosEstacionamentoValidation()
    {
        RuleFor(horario => horario.VeiculoId)
            .NotEmpty().WithMessage("O campo VeiculoId é obrigatório");

        RuleFor(horario => horario.Data)
                .InclusiveBetween(new DateTime(1900, 1, 1), new DateTime(2100, 12, 31))
                .WithMessage("O ano deve estar no intervalo de 1900 a 2100");
    }
}

