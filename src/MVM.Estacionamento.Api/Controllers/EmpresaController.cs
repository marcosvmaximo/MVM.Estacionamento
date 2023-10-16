using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVM.Estacionamento.Api.ViewModels;
using MVM.Estacionamento.Business.Interfaces;
using MVM.Estacionamento.Business.Models;
using MVM.Estacionamento.Business.Services.Common;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Api.Controllers;

[Route("api/empresa")]
public class EmpresaController : MainController
{
    private readonly IEmpresaService _service;
    private readonly IEmpresaRepository _repository;

    public EmpresaController(IMapper mapper,
                             INotifyBus bus,
                             IEmpresaService service,
                             IEmpresaRepository repository)
        : base(bus, mapper)
    {
        _service = service;
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmpresaViewModel>>> ObterTodas()
    {
        var result = await _repository.ObterTodos();

        if (result == null || !result.Any())
            await Notify(HttpStatusCode.NotFound.ToString(), "");

        return await CustomResponse(_mapper.Map<IEnumerable<EmpresaViewModel>>(result));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmpresaViewModel>> ObterPorId([FromRoute] Guid id)
    {
        var result = await _repository.ObterPorId(id);

        if (result == null)
            await Notify(HttpStatusCode.NotFound.ToString(), "");

        return await CustomResponse(_mapper.Map<EmpresaViewModel>(result));
    }

    [HttpGet("cnpj/{cnpj}")]
    public async Task<ActionResult<EmpresaViewModel>> ObterPorCnpj(string cnpj)
    {
        if (cnpj.Length != 14 || string.IsNullOrWhiteSpace(cnpj))
            await Notify(nameof(Empresa), "Cnpj inválido.");

        var result = await _repository.Buscar(e => e.Cnpj == cnpj);

        if (result == null || !result.Any())
            await Notify(HttpStatusCode.NotFound.ToString(), "");

        return await CustomResponse(_mapper.Map<IEnumerable<EmpresaViewModel>>(result));
    }

    [HttpPost]
    public async Task<ActionResult> Inserir([FromBody] EmpresaViewModel empresaViewModel)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        var empresa = _mapper.Map<Empresa>(empresaViewModel);
        await _service.AdicionarEmpresa(empresa);

        return await CustomResponse(empresaViewModel);
    }

    [HttpPut]
    public async Task<ActionResult> Atualizar([FromBody] EmpresaViewModel empresaViewModel)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        var empresa = _mapper.Map<Empresa>(empresaViewModel);
        await _service.AtualizarDadosEmpresa(empresa);

        return await CustomResponse(empresaViewModel);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Remover([FromRoute] Guid id)
    {
        await _service.DeletarEmpresa(id);

        return await CustomResponse();
    }
}

