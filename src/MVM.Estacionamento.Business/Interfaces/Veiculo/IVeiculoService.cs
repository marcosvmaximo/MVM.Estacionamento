using System;
using MVM.Estacionamento.Business.Models;

namespace MVM.Estacionamento.Business.Interfaces.VeiculoContext;

public interface IVeiculoService
{
    Task RegistrarEntradaVeiculo(Veiculo veiculo, RegistroEstacionamento horario);
    Task RegistrarSaidaVeiculo(Guid idEmpresa, Guid idVeiculo);
}

