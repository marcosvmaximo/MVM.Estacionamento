using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MVM.Estacionamento.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace MVM.Estacionamento.Api.Configuration;

public static class ServicesIdentity
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}

