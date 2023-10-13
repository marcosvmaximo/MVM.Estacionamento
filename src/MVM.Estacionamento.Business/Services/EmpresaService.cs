using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using MVM.Estacionamento.Business.Interfaces;
using MVM.Estacionamento.Business.Models;
using MVM.Estacionamento.Business.Services.Common;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Business.Services;

public class EmpresaService : BaseService, IEmpresaService
{
    private readonly IEmpresaRepository _repository;

    public EmpresaService(IEmpresaRepository repository, INotifyBus bus)
        : base(bus)
    {
        _repository = repository;
    }

    public async Task AdicionarEmpresa(Empresa empresa)
    {
        if (!Validar(empresa))
            return;

        var empresaExiste = await _repository.Buscar(x => x.Cnpj == empresa.Cnpj || x.Id == empresa.Id);
        if (empresaExiste.Any())
        {
            Notificar(empresa.Nome, "Empresa já existente");
            return;
        }

        await _repository.Adicionar(empresa);
    }

    public async Task AtualizarDadosEmpresa(Empresa empresa)
    {
        var empresaExiste = await _repository.Buscar(e => e.Id == empresa.Id);
        if (!empresaExiste.Any())
        {
            Notificar(HttpStatusCode.NotFound.ToString(), "");
            return;
        }

        if (!Validar(empresa))
            return;

        await _repository.Atualizar(empresa);
    }

    public async Task DeletarEmpresa(Guid id)
    {
        // Empresa não deve ser deletada se houver carros cadastrados a ela
        var empresa = await _repository.ObterEmpresaComVeiculos(id);

        if (empresa == null)
        {
            Notificar(HttpStatusCode.NotFound.ToString(), "Empresa não encontrada");
            return;
        }

        if (empresa.Veiculos.Any())
        {
            Notificar(empresa.Nome, $"A Empresa {empresa.Nome} não pode ser removida, pois existem carros relacionados à ela.");
            return;
        }

        await _repository.Remover(empresa);
    }

    private bool Validar(Empresa empresa)
    {
        empresa.Validar();

        if (!empresa.ValidationResult.IsValid)
        {
            Notificar(empresa.ValidationResult);
            return false;
        }

        return true;
    }
}

