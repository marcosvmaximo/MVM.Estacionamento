using MVM.Estacionamento.Business.Models;

namespace MVM.Estacionamento.Business.Interfaces.VeiculoContext;
public interface IVeiculoRepository : IRepository<Veiculo>
{
    Task<Veiculo?> ObterVeiculoComHorarios(Guid id);
    Task<IEnumerable<Veiculo?>> ObterTodosVeiculoComHorarios();
    Task<IEnumerable<Veiculo?>> ObterTodosVeiculoComHorariosPorEmpresa(Guid idEmpresa);
    Task Atualizar(RegistroEstacionamento horario);
}

