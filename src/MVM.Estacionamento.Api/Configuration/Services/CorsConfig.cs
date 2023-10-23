using System;
namespace MVM.Estacionamento.Api.Configuration.Services;

public static class CorsConfig
{
    public const string ENV_DEV = "dev";
    public const string ENV_PROD = "prod";

    public static IServiceCollection AddCorsConfig(this IServiceCollection services)
    {
        services.AddCors(builder =>
        {
            builder.AddPolicy(ENV_DEV, opt =>
            {
                opt.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
            });

            builder.AddPolicy(ENV_PROD, opt =>
            {
                opt.WithOrigins("https://localhost:5000/", "https://localhost:5001/")
                   .SetIsOriginAllowedToAllowWildcardSubdomains()
                   .WithMethods("GET", "POST", "PUT")
                   .AllowAnyHeader();
            });
        });

        return services;
    }

    public static IApplicationBuilder UseCorsConfig(this IApplicationBuilder builder, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            builder.UseCors(ENV_DEV);
        }

        builder.UseCors(ENV_PROD);

        return builder;
    }
}

