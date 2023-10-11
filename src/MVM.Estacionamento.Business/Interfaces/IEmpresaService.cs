using System;
using MVM.Estacionamento.Business.Models;

namespace MVM.Estacionamento.Business.Services.Common;

public interface IEmpresaService
{
    Task<Empresa> AdicionarEmpresa(Empresa empresa);
    Task<Empresa> AtualizarDadosEmpresa(Guid id, Empresa empresa);
    Task DeletarEmpresa(Guid id);
}

