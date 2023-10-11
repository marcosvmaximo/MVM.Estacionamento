using System;
using Microsoft.EntityFrameworkCore;
using MVM.Estacionamento.Business.Models;

namespace MVM.Estacionamento.Data.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> opt) : base(opt)
    {
    }

    DbSet<Empresa> Empresas { get; set; }
    DbSet<Veiculo> Veiculos { get; set; }
}

