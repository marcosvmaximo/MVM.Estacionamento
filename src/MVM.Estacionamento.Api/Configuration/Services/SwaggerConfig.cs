using System;
using Microsoft.OpenApi.Models;

namespace MVM.Estacionamento.Api.Configuration.Services;

public static class SwaggerConfig
{
    // Outra configuração faltante é a abertura/fechamento da API(Swagger), para terceiros..
    //
    // Podemos fazer isso simplismente adicionando um Middleware que verifica se o usuário tentando acessar a API
    // está ou não autenticado, isso antes de chamar o swagger. E conforme a resposta mostra ou não a tela.
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            // Configura versão 1
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API MVM.Estacionamento v1.0",
                Version = "v1",
                Description = "Api gerênciadora de empresas de estacionamentos e relacionados.",
                Contact = new OpenApiContact
                {
                    Name = "Marcos Maximo",
                    Email = "marcosvinmaximo@gmail.com",
                    Url = new Uri("https://github.com/marcosvmaximo")
                },
                License = new OpenApiLicense
                {
                    Name = "Marcos Maximo",
                    Url = new Uri("https://github.com/marcosvmaximo")
                }
            });
            // Configura versão 2
            c.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "API MVM.Estacionamento v2.0",
                Version = "v2",
                Description = "Api gerênciadora de empresas de estacionamentos e relacionados.",
                Contact = new OpenApiContact
                {
                    Name = "Marcos M",
                    Url = new Uri("https://github.com/marcosvmaximo")
                },
                License = new OpenApiLicense
                {
                    Name = "MVM License",
                    Url = new Uri("https://github.com/marcosvmaximo")
                }
            });

            // Inclui o XML de documentação
            var xmlFile = "MVM.Estacionamento.Api.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "Insira o JWT desta maneira: Bearer {seuToken}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });


        return services;
    }

    public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder builder, IWebHostEnvironment env)
    {
        builder.UseSwagger();
        builder.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Version 1.0");
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "API Version 2.0");
        });
        return builder;
    }
}

