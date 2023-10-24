using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MVM.Estacionamento.Api.Configuration;
using MVM.Estacionamento.Api.Configuration.Middlewares;
using MVM.Estacionamento.Api.Configuration.Services;
using MVM.Estacionamento.Configuration;
using MVM.Estacionamento.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddServicesExtensions(builder.Configuration);

// Contexts
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt
     .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDbContext<DataContext>(opt => opt
    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// SwaggerConfig
app.UseSwaggerConfig(app.Environment);

// Middlewares
app.UseMiddleware<ExceptionMiddleware>();

// Cors
app.UseCorsConfig(app.Environment);

// Https
app.UseHsts();
app.UseHttpsRedirection();

// Auth
app.UseAuthentication();
app.UseAuthorization();

// Logging
app.UseElmahIo();
app.UseHealthCheckConfig();

// Handler padrão para exceções do .net
//app.UseExceptionHandler("/api/error");

app.MapControllers();
app.Run();

