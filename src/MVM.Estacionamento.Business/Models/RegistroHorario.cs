using System;
using MVM.Estacionamento.Business.Validations;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Business.Models;

public class RegistroEstacionamento : Entity
{
    public RegistroEstacionamento()
    {
    }

    public RegistroEstacionamento(Guid veiculoId, TimeOnly horarioEntrada)
    {
        VeiculoId = veiculoId;
        Data = DateTime.Now;
        HorarioEntrada = horarioEntrada;
    }

    public Guid VeiculoId { get; set; }
    public DateTime Data { get; set; }
    public TimeOnly HorarioEntrada { get; set; }
    public TimeOnly HorarioSaida { get; set; }
    public TimeSpan TempoUtilizado { get; set; }

    // Ef Re..
    public Veiculo Veiculo { get; set; }

    public void MarcarSaida()
    {
        HorarioSaida = TimeOnly.FromDateTime(DateTime.Now);
        TempoUtilizado = HorarioSaida - HorarioEntrada;
    }

    public override void Validar()
    {
        ValidationResult = Validate<RegistrosEstacionamentoValidation, RegistroEstacionamento>();
    }
}

