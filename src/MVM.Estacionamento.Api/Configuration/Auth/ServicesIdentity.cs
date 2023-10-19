using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MVM.Estacionamento.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using MVM.Estacionamento.Api.Configuration.Auth;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MVM.Estacionamento.Api.Configuration;

public static class ServicesIdentity
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddErrorDescriber<ErrorDescribersPortugues>()
            .AddDefaultTokenProviders();


        // JWT
        var jwtConfigInfos = configuration.GetSection("JwtConfig"); // Obtenho dados do appSettings
        services.Configure<JwtConfiguration>(jwtConfigInfos); // Converto os dados para o objeto em qualquer dependencia

        var jwtConfig = jwtConfigInfos.Get<JwtConfiguration>();
        var key = Encoding.ASCII.GetBytes(jwtConfig?.Secret);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = jwtConfig.ValidoEm,
                ValidIssuer = jwtConfig.Emissor
            };
        });

        return services;
    }
}

