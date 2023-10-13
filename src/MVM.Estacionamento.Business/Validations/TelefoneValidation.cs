using System;
using FluentValidation;
using MVM.Estacionamento.Business.ValueObjects;

namespace MVM.Estacionamento.Business.Validations;

public class TelefoneValidation : AbstractValidator<Telefone>
{
    public TelefoneValidation()
    {
        RuleFor(telefone => telefone.Ddd)
            .NotEmpty().WithMessage("O DDD é obrigatório.")
            .Length(2).WithMessage("O DDD deve conter 2 dígitos.");

        RuleFor(telefone => telefone.TelefoneCelular)
            .NotEmpty().WithMessage("O telefone celular é obrigatório.")
            .Matches(@"^\d{8,9}$").WithMessage("O telefone celular deve conter de 8 a 9 dígitos numéricos.");

        RuleFor(telefone => telefone.TelefoneFixo)
            .Matches(@"^\d{8,9}$").WithMessage("O telefone fixo deve conter de 8 a 9 dígitos numéricos.");
    }
}

