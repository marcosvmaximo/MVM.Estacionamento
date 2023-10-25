using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MVM.Estacionamento.Api.Configuration.HealthChecks;

namespace MVM.Estacionamento.Api.Configuration.Services;

public static class HealthCheckConfig
{
    public static IServiceCollection AddHealthCheckConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var keysLogging = GetLoggingConfig(services, configuration);

        services.AddHealthChecks()
            .AddCheck(name: "Empresa", new HealthCheckMySql(connectionString!))
            .AddMySql(connectionString!, name: "Banco de dados")
            .AddElmahIoPublisher(opt =>
            {
                opt.ApiKey = keysLogging!.ApiKey;
                opt.LogId = keysLogging.LogId;
                opt.Application = "API Estacionamentos";
            });

        services.AddHealthChecksUI(options =>
        {
            options.SetEvaluationTimeInSeconds(5);
            options.MaximumHistoryEntriesPerEndpoint(10);
            options.AddHealthCheckEndpoint("API com Health Checks", "/api/hc");
        }).AddInMemoryStorage(); //Aqui adicionamos o banco em memória


        return services;
    }

    private static LoggingConfigModel? GetLoggingConfig(IServiceCollection services, IConfiguration configuration)
    {
        var elmahSection = configuration.GetSection("Logging").GetSection("ElmahIo");
        services.Configure<LoggingConfigModel>(elmahSection);

        return elmahSection.Get<LoggingConfigModel>();
    }

    public static IApplicationBuilder UseHealthCheckConfig(this IApplicationBuilder builder)
    {
        builder.UseHealthChecks("/api/hc", new HealthCheckOptions
        {
            Predicate = p => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        builder.UseHealthChecksUI(options =>
        {
            options.UIPath = "/api/hc-ui";
            options.ApiPath = "/api/hc-ui" + "/api";
            options.ResourcesPath = "/api/hc-ui" + "/resources";
            options.WebhookPath = "/api/hc-ui" + "/webhooks";

            options.UseRelativeApiPath = false;
            options.UseRelativeResourcesPath = false;
            options.UseRelativeWebhookPath = false;

            //options.UIPath = "/api/hc-ui";
            //options.ResourcesPath = "/ui/resources";
        });

        return builder;
    }
}

public record LoggingConfigModel(string ApiKey, Guid LogId);
