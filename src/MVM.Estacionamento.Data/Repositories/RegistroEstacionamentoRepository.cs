using System;
using MVM.Estacionamento.Business.Interfaces.VeiculoContext;
using MVM.Estacionamento.Business.Models;
using MVM.Estacionamento.Data.Context;

namespace MVM.Estacionamento.Data.Repositories;

public class RegistroEstacionamentoRepository : Repository<RegistroEstacionamento>, IRegistroEstacionamentoRepository
{
    public RegistroEstacionamentoRepository(DataContext context) : base(context)
    {
    }
}

