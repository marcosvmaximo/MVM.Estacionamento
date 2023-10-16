using System;
using MVM.Estacionamento.Business.Enum;
using MVM.Estacionamento.Business.Validations;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Business.Models;

public class Veiculo : Entity
{
    private List<RegistroEstacionamento> _registrosEstacionamento;

    public Veiculo()
    {
        _registrosEstacionamento = new List<RegistroEstacionamento>();
    }

    public Veiculo(Guid empresaId, string marca, string modelo, string cor, string placa, DateTime ano, ETipoVeiculo tipo)
    {
        EmpresaId = empresaId;
        Marca = marca;
        Modelo = modelo;
        Cor = cor;
        Placa = placa;
        Ano = ano;
        Tipo = tipo;
        Status = false;

        _registrosEstacionamento = new List<RegistroEstacionamento>();
    }

    public Guid EmpresaId { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Cor { get; set; }
    public string Placa { get; set; }
    public DateTime Ano { get; set; }
    public ETipoVeiculo Tipo { get; set; }
    public bool Status { get; set; }

    // Ef Relation
    public Empresa Empresa { get; set; }
    public IEnumerable<RegistroEstacionamento> RegistrosEstacionamento => _registrosEstacionamento;

    public override void Validar()
    {
        ValidationResult = Validate<VeiculoValidation, Veiculo>();
    }

    public void Ativar()
    {
        Status = true;
    }

    public void RegitrarEntradaEstacionamento(RegistroEstacionamento horario)
    {
        if (horario == null)
            throw new ArgumentNullException();

        Status = true;
        _registrosEstacionamento.Add(horario);
    }
}