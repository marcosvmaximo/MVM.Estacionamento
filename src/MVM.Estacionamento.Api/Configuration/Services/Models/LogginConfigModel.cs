using System;
namespace MVM.Estacionamento.Api.Configuration.Services.Models;

public class LoggingConfigModel
{
    public LoggingConfigModel()
    {
        ApiKey = Guid.Empty.ToString();
        LogId = Guid.Empty.ToString();
    }

    public string ApiKey { get; set; }
    public string LogId { get; set; }
}

