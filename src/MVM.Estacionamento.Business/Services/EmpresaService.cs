using System;
using System.ComponentModel.DataAnnotations;
using MVM.Estacionamento.Business.Interfaces;
using MVM.Estacionamento.Business.Models;
using MVM.Estacionamento.Business.Services.Common;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Business.Services;

public class EmpresaService : BaseService, IEmpresaService
{
    private readonly IEmpresaRepository _repository;

    public EmpresaService(IEmpresaRepository repository, IMediatorHandler bus)
        : base(bus)
    {
        _repository = repository;
    }

    public async Task<Empresa> AdicionarEmpresa(Empresa empresa)
    {
        Validar(empresa);

        var empresaExiste = await _repository.Buscar(x => x.Cnpj == empresa.Cnpj || x.Id == empresa.Id);
        if (empresaExiste.Any())
        {
            Notificar(empresa.Nome, "Empresa já existente");
            return null;
        }

        return await _repository.Adicionar(empresa);
    }

    public async Task<Empresa> AtualizarDadosEmpresa(Guid id, Empresa empresa)
    {
        Validar(empresa);

        if (id != empresa.Id)
        {
            Notificar(empresa.Nome, "Id incorreto");
            return null;
        }

        await _repository.Atualizar(empresa);
        return empresa;
    }

    public async Task DeletarEmpresa(Guid id)
    {
        // Empresa não deve ser deletada se houver carros cadastrados a ela
        var empresa = await _repository.ObterEmpresaComVeiculos(id);

        if (empresa == null || empresa.Veiculos.Any())
        {
            Notificar(empresa.Nome, $"A Empresa {empresa.Nome} não pode ser removida enquanto houver carros inseridos a ela");
            return;
        }

        await _repository.Remover(id);
    }

    private void Validar(Empresa empresa)
    {
        if (!empresa.ValidationResult.IsValid)
            Notificar(empresa.ValidationResult);
    }
}

