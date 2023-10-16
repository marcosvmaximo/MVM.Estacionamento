using Microsoft.EntityFrameworkCore;
using MVM.Estacionamento.Business.Interfaces.VeiculoContext;
using MVM.Estacionamento.Business.Models;
using MVM.Estacionamento.Data.Context;

namespace MVM.Estacionamento.Data.Repositories;

public class VeiculoRepository : Repository<Veiculo>, IVeiculoRepository
{
    public VeiculoRepository(DataContext context) : base(context)
    {
    }

    public async Task Atualizar(RegistroEstacionamento horario)
    {
        _context.Update(horario);
        _context.SaveChanges();
    }

    public async Task<IEnumerable<Veiculo?>> ObterTodosVeiculoComHorarios()
    {
        return _dbSet.AsNoTracking()
            .Include(x => x.RegistrosEstacionamento)
            .ToList();
    }

    public async Task<IEnumerable<Veiculo?>> ObterTodosVeiculoComHorariosPorEmpresa(Guid idEmpresa)
    {
        return _dbSet.AsNoTracking()
            .Include(x => x.RegistrosEstacionamento)
            .Where(x => x.EmpresaId == idEmpresa)
            .ToList();
    }

    public async Task<Veiculo?> ObterVeiculoComHorarios(Guid id)
    {
        return _dbSet.AsNoTracking()
            .Include(x => x.RegistrosEstacionamento)
            .FirstOrDefault(x => x.Id == id);
    }
}

