using System;
using Elmah.Io.AspNetCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySqlConnector;

namespace MVM.Estacionamento.Api.Configuration.HealthChecks;

public class HealthCheckMySql : IHealthCheck
{
    private readonly string _connection;

    public HealthCheckMySql(string connectionString)
    {
        _connection = connectionString;
    }

    /// <summary>
    /// Deve possuir itens na tabela de empresas, se não houver, está com algum problema.
    /// </summary>
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            using (var connection = new MySqlConnection(_connection))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT count(id) FROM Empresas";

                var result = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0 ?
                    HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();

                return result;
            }
        }
        catch (Exception)
        {
            return HealthCheckResult.Unhealthy();
        }

    }
}

