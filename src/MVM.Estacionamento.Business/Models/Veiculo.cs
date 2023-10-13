using System;
using MVM.Estacionamento.Business.Enum;
using MVM.Estacionamento.Business.Validations;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Business.Models;

public class Veiculo : Entity
{
    public Veiculo()
    {
    }

    public Guid EmpresaId { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Cor { get; set; }
    public string Placa { get; set; }
    public ETipoVeiculo Tipo { get; set; }

    // Ef Relation
    public Empresa Empresa { get; set; }

    public void Validar()
    {
        Validate<VeiculoValidation, Veiculo>();
    }
}