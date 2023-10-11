using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVM.Estacionamento.Api.ViewModels;
using MVM.Estacionamento.Business.Interfaces;
using MVM.Estacionamento.Business.Services.Common;

namespace MVM.Estacionamento.Api.Controllers;

[Route("api/[controller]")]
public class EmpresaController : MainController
{
    private readonly IMapper _mapper;
    private readonly IEmpresaService _service;
    private readonly IEmpresaRepository _repository;

    public EmpresaController(IMapper mapper,
                             IEmpresaService service,
                             IEmpresaRepository repository)
    {
        _mapper = mapper;
        _service = service;
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmpresaViewModel>>> ObterTodas()
    {
        var result = await _repository.ObterTodos();

        if (result == null)
            return NotFound();

        return Ok(_mapper.Map<IEnumerable<EmpresaViewModel>>(result));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmpresaViewModel>> ObterPorId([FromRoute] Guid id)
    {
        var result = await _repository.ObterPorId(id);

        if (result == null)
            return NotFound();

        return Ok(_mapper.Map<EmpresaViewModel>(result));
    }

    [HttpPost]
    public async Task<ActionResult> Inserir([FromBody] EmpresaViewModel empresaViewModel)
    {
        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Atualizar([FromRoute] Guid id, [FromBody] EmpresaViewModel empresaViewModel)
    {
        return Ok();

    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Remover([FromRoute] Guid id)
    {
        return Ok();

    }
}

