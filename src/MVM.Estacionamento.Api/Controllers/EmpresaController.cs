using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVM.Estacionamento.Api.Configuration.Auth;
using MVM.Estacionamento.Api.Controllers.Common;
using MVM.Estacionamento.Api.ViewModels;
using MVM.Estacionamento.Business.Interfaces;
using MVM.Estacionamento.Business.Models;
using MVM.Estacionamento.Business.Services.Common;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Api.Controllers;


//
// Necessário ajustar a documentação no swagger
//  - Permitir/Bloquear Acessos externos
//  - Criar autenticação via swagger
//  - Ajustar métodos depretch
[Authorize]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/empresa")]
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

    /// <summary>
    /// Obtem todas empresas cadastradas.
    /// </summary>
    /// <returns>Uma lista de Empresas</returns>
    /// 
    /// <remarks>
    /// Busca no BD todas empresas, retornando então uma lista com dados possíveis.
    /// <br></br>
    /// <br></br>
    /// Objeto padrão das respostas: <br></br>
    ///     "httpCode" : 200,<br></br>
    ///     "sucess" : true,<br></br>
    ///     "message" "Requisição enviada com sucesso.",<br></br>
    ///     "result": {}<br></br>
    /// </remarks>
    /// <response code="200">Sucesso: Retorna uma lista de Empresas</response>
    /// <response code="400">Falha: Se ocorreu algum problema ao buscar a lista</response>
    /// <response code="404">Não encontrado: Se a lista estiver vázia</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmpresaViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse))]
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
    [ClaimsAuthorized("Gerente", "Inserir")]
    public async Task<ActionResult> Inserir([FromBody] EmpresaViewModel empresaViewModel)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        var empresa = _mapper.Map<Empresa>(empresaViewModel);
        await _service.AdicionarEmpresa(empresa);

        return await CustomResponse(empresaViewModel);
    }

    [HttpPut]
    [ClaimsAuthorized("Gerente", "Atualizar")]
    public async Task<ActionResult> Atualizar([FromBody] EmpresaViewModel empresaViewModel)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        var empresa = _mapper.Map<Empresa>(empresaViewModel);
        await _service.AtualizarDadosEmpresa(empresa);

        return await CustomResponse(empresaViewModel);
    }

    [HttpDelete("{id:guid}")]
    [ClaimsAuthorized("Gerente", "Excluir")]
    public async Task<ActionResult> Remover([FromRoute] Guid id)
    {
        await _service.DeletarEmpresa(id);

        return await CustomResponse();
    }
}

