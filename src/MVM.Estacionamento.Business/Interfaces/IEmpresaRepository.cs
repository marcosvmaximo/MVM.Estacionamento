using System;
using MVM.Estacionamento.Business.Models;

namespace MVM.Estacionamento.Business.Interfaces;

public interface IEmpresaRepository : IRepository<Empresa>
{
    Task<Empresa?> ObterEmpresaComVeiculos(Guid id);
}

