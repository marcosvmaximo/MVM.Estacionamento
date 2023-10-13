using System;
using Microsoft.EntityFrameworkCore;
using MVM.Estacionamento.Business.Interfaces;
using MVM.Estacionamento.Business.Models;
using MVM.Estacionamento.Data.Context;

namespace MVM.Estacionamento.Data.Repositories;

public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
{
    public EmpresaRepository(DataContext context) : base(context)
    {
    }

    public async Task<Empresa?> ObterEmpresaComVeiculos(Guid id)
    {
        return await _dbSet
            .Include(x => x.Veiculos)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}

