using Elmah.Io.Extensions.Logging;

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
            o.LogId = keys.LogId;
        });
        services.AddLogging(builder =>
        {
            builder.AddElmahIo(o =>
            {
                o.ApiKey = keys!.ApiKey;
                o.LogId = keys.LogId;
            });

            builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
        });

        return services;
    }
}

