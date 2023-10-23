using System;
using Microsoft.AspNetCore.Mvc;

namespace MVM.Estacionamento.Api.Configuration.Services;

public static class VersionConfig
{
    public static IServiceCollection AddVersionConfig(this IServiceCollection services)
    {
        // Configura versionamento
        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
        });

        services.AddVersionedApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'VVV";
            opt.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}

