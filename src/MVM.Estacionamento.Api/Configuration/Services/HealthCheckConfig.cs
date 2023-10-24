using System;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace MVM.Estacionamento.Api.Configuration.Services;

public static class HealthCheckConfig
{
    public static IServiceCollection AddHealthCheckConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddHealthChecks()
            .AddMySql(connectionString!, name: "Banco de dados");

        services.AddHealthChecksUI(options =>
        {
            options.SetEvaluationTimeInSeconds(5);
            options.MaximumHistoryEntriesPerEndpoint(10);
            options.AddHealthCheckEndpoint("API com Health Checks", "/api/hc");
        }).AddInMemoryStorage(); //Aqui adicionamos o banco em memória


        return services;
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

