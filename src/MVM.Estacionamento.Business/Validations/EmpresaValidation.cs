using System;
using FluentValidation;
using MVM.Estacionamento.Business.Models;

namespace MVM.Estacionamento.Business.Validations;

public class EmpresaValidation : AbstractValidator<Empresa>
{
    public EmpresaValidation()
    {
        RuleFor(empresa => empresa.Nome)
            .NotEmpty().WithMessage("O nome da empresa é obrigatório.")
            .MaximumLength(100).WithMessage("O nome da empresa deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Cnpj)
            .NotEmpty().WithMessage("O CNPJ é obrigatório.")
            .Length(14).WithMessage("O CNPJ deve conter 14 dígitos.")
            .Must(CnpjValidator).WithMessage("Cnpj informado, não corresponde a um CNPJ valido.");

        RuleFor(x => x.Telefone)
            .SetValidator(new TelefoneValidation());

        RuleFor(x => x.Endereco)
            .SetValidator(new EnderecoValidation());

        RuleFor(empresa => empresa.QuantidadeVagasMotos)
            .GreaterThanOrEqualTo(0).WithMessage("A quantidade de vagas para motos deve ser maior ou igual a zero.");

        RuleFor(empresa => empresa.QuantidadeVagasCarros)
            .GreaterThanOrEqualTo(0).WithMessage("A quantidade de vagas para carros deve ser maior ou igual a zero.");
    }

    public bool CnpjValidator(string cnpj)
    {
        // Remove caracteres não numéricos do CNPJ
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        // Verifica se o CNPJ tem 14 dígitos
        if (cnpj.Length != 14)
        {
            return false;
        }

        // Calcula e verifica os dígitos verificadores
        int[] mult1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int sum1 = 0;

        for (int i = 0; i < 12; i++)
        {
            sum1 += int.Parse(cnpj[i].ToString()) * mult1[i];
        }

        int remainder1 = sum1 % 11;
        int digit1 = remainder1 < 2 ? 0 : 11 - remainder1;

        if (int.Parse(cnpj[12].ToString()) != digit1)
        {
            return false;
        }

        int[] mult2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int sum2 = 0;

        for (int i = 0; i < 13; i++)
        {
            sum2 += int.Parse(cnpj[i].ToString()) * mult2[i];
        }

        int remainder2 = sum2 % 11;
        int digit2 = remainder2 < 2 ? 0 : 11 - remainder2;

        return int.Parse(cnpj[13].ToString()) == digit2;
    }
}

