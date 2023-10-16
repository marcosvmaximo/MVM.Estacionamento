using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MVM.Estacionamento.Api.ViewModels.Veiculo;

public class RegistroEstacionamentoViewModel
{
    [Key]
    public Guid Id { get; set; }

    public DateTime Data { get; set; }

    public TimeOnly HorarioEntrada { get; set; }

    public TimeOnly HorarioSaida { get; set; }

    public TimeSpan TempoUtilizado { get; set; }
}

