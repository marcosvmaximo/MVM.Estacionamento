using System;
using FluentValidation;
using MVM.Estacionamento.Business.ValueObjects;

namespace MVM.Estacionamento.Business.Validations;

public class EnderecoValidation : AbstractValidator<Endereco>
{
    public EnderecoValidation()
    {
        RuleFor(endereco => endereco.Cep)
             .NotEmpty().WithMessage("O CEP é obrigatório.")
             .Length(8).WithMessage("O CEP deve conter 8 dígitos.");

        RuleFor(endereco => endereco.Logradouro)
            .NotEmpty().WithMessage("O logradouro é obrigatório.")
            .MaximumLength(100).WithMessage("O logradouro deve ter no máximo 100 caracteres.");

        RuleFor(endereco => endereco.Complemento)
            .MaximumLength(50).WithMessage("O complemento deve ter no máximo 50 caracteres.");

        RuleFor(endereco => endereco.Numero)
            .NotEmpty().WithMessage("O número é obrigatório.")
            .GreaterThan(0).WithMessage("O número deve ser maior que zero.");

        RuleFor(endereco => endereco.Bairro)
            .NotEmpty().WithMessage("O bairro é obrigatório.")
            .MaximumLength(50).WithMessage("O bairro deve ter no máximo 50 caracteres.");

        RuleFor(endereco => endereco.Cidade)
            .NotEmpty().WithMessage("A cidade é obrigatória.")
            .MaximumLength(50).WithMessage("A cidade deve ter no máximo 50 caracteres.");
    }
}

