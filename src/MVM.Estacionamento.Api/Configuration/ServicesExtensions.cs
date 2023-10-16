using MVM.Estacionamento.Api.Configuration;
using MVM.Estacionamento.Api.Controllers;
using MVM.Estacionamento.Business.Interfaces;
using MVM.Estacionamento.Business.Interfaces.VeiculoContext;
using MVM.Estacionamento.Business.Services;
using MVM.Estacionamento.Business.Services.Common;
using MVM.Estacionamento.Core;
using MVM.Estacionamento.Data.Context;
using MVM.Estacionamento.Data.Repositories;

namespace MVM.Estacionamento.Configuration;

public static class ServicesExtensions
{
    public static IServiceCollection AddServicesExtensions(this IServiceCollection services)
    {
        services.AddScoped<DataContext>();

        services.AddAutoMapper(typeof(MainController));
        services.AddAutoMapper(typeof(EmpresaProfile));

        services.AddTransient<IEmpresaRepository, EmpresaRepository>();
        services.AddTransient<IEmpresaService, EmpresaService>();
        services.AddTransient<IVeiculoService, VeiculoService>();
        services.AddTransient<IVeiculoRepository, VeiculoRepository>();
        services.AddTransient<IRegistroEstacionamentoRepository, RegistroEstacionamentoRepository>();

        services.AddScoped<INotifyBus, NotifyBus>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}

