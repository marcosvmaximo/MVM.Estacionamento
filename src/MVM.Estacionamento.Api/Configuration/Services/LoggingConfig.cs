using System;
using Elmah.Io.Extensions.Logging;

namespace MVM.Estacionamento.Api.Configuration.Services;

public static class LoggingConfig
{
    public static IServiceCollection AddLoggingConfig(this IServiceCollection services)
    {
        services.AddElmahIo(o =>
        {
            o.ApiKey = "ef2c6247c3544893a3706f1972024a12";
            o.LogId = new Guid("6985adb2-f94b-4861-872e-1941bd6e3650");
        });
        services.AddLogging(builder =>
        {
            builder.AddElmahIo(o =>
            {
                o.ApiKey = "ef2c6247c3544893a3706f1972024a12";
                o.LogId = new Guid("6985adb2-f94b-4861-872e-1941bd6e3650");
            });

            builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
        });

        return services;
    }
}

