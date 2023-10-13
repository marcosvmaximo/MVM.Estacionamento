using System;
using MVM.Estacionamento.Business.Models;

namespace MVM.Estacionamento.Business.Services.Common;

public interface IEmpresaService
{
    Task AdicionarEmpresa(Empresa empresa);
    Task AtualizarDadosEmpresa(Empresa empresa);
    Task DeletarEmpresa(Guid id);
}

