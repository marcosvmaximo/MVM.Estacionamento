using System;
using Elmah.Io.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using MVM.Estacionamento.Api.Configuration.Services.Models;

namespace MVM.Estacionamento.Api.Configuration.Services;

public static class LoggingConfig
{
    public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var elmahSection = configuration.GetSection("Logging").GetSection("ElmahIo");

        services.Configure<LoggingConfigModel>(elmahSection);
        LoggingConfigModel? keys = elmahSection.Get<LoggingConfigModel>();

        services.AddElmahIo(o =>
        {
            o.ApiKey = keys!.ApiKey;
            o.LogId = new Guid(keys.LogId);
        });
        services.AddLogging(builder =>
        {
            builder.AddElmahIo(o =>
            {
                o.ApiKey = keys!.ApiKey;
                o.LogId = new Guid(keys.LogId);
            });

            builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
        });

        return services;
    }
}

