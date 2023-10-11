using System;
using FluentValidation;
using MVM.Estacionamento.Business.Models;

namespace MVM.Estacionamento.Business.Validations;

public class VeiculoValidation : AbstractValidator<Veiculo>
{
    public VeiculoValidation()
    {
    }
}

