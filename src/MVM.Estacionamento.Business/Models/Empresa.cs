using System;
using FluentValidation.Results;
using MVM.Estacionamento.Business.Enum;
using MVM.Estacionamento.Business.Validations;
using MVM.Estacionamento.Business.ValueObjects;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Business.Models;

public class Empresa : Entity
{
    private List<Veiculo> _veiculos;

    public Empresa()
    {
        _veiculos = new List<Veiculo>();
    }

    public string Nome { get; set; }
    public string Cnpj { get; set; }
    public Endereco Endereco { get; set; }
    public Telefone Telefone { get; set; }
    public int QuantidadeVagasMotos { get; set; }
    public int QuantidadeVagasCarros { get; set; }

    // Ef Relation
    public IEnumerable<Veiculo> Veiculos => _veiculos;

    public override void Validar()
    {
        ValidationResult = Validate<EmpresaValidation, Empresa>()!;
    }

    public bool PodeEstacionar(Veiculo veiculo)
    {
        if (veiculo.Tipo == ETipoVeiculo.Moto || veiculo.Tipo == ETipoVeiculo.Bicicleta)
        {
            if (QuantidadeVagasMotos < 1)
                return false;

            return true;
        }

        if (QuantidadeVagasCarros < 1)
            return false;

        return true;
    }

    // Correto seria esse unico metodo cuidar de todas validações envolvendo a inserção de um veiculo
    public void AdicionarVeiculo(Veiculo veiculo)
    {
        if (veiculo == null)
            throw new ArgumentNullException();

        _veiculos.Add(veiculo);
    }

    //private void DiminuirEstoque(Veiculo veiculo)
    //{
    //    if (veiculo.Tipo == ETipoVeiculo.Moto || veiculo.Tipo == ETipoVeiculo.Bicicleta)
    //        QuantidadeVagasMotos--;
    //    else
    //        QuantidadeVagasCarros--;
    //}
}