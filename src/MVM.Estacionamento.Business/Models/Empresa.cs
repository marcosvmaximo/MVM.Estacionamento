using System;
using FluentValidation.Results;
using MVM.Estacionamento.Business.Validations;
using MVM.Estacionamento.Business.ValueObjects;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Business.Models;

public class Empresa : Entity
{
    public Empresa()
    {
        ValidationResult = Validate<EmpresaValidation, Empresa>()!;
    }

    public string Nome { get; set; }
    public string Cnpj { get; set; }
    public Endereco Endereco { get; set; }
    public Telefone Telefone { get; set; }
    public int QuantidadeVagasMotos { get; set; }
    public int QuantidadeVagasCarros { get; set; }

    // Ef Relation
    public IEnumerable<Veiculo> Veiculos { get; set; }
}