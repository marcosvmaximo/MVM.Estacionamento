using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVM.Estacionamento.Api.ViewModels;
using MVM.Estacionamento.Api.ViewModels.Veiculo;
using MVM.Estacionamento.Business.Interfaces.VeiculoContext;
using MVM.Estacionamento.Business.Models;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Api.Controllers;

[Route("api/veiculo")]
public class VeiculoController : MainController
{
    private readonly IVeiculoRepository _repository;
    private readonly IVeiculoService _service;

    public VeiculoController(INotifyBus notifyBus,
                             IMapper mapper,
                             IVeiculoRepository repository,
                             IVeiculoService service) : base(notifyBus, mapper)
    {
        _repository = repository;
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<VeiculoViewModel>> ObterTodosVeiculos()
    {
        var veiculos = await _repository.ObterTodosVeiculoComHorarios();

        if (veiculos == null || !veiculos.Any())
            await Notify(HttpStatusCode.NotFound.ToString(), "");

        return await CustomResponse(_mapper.Map<IEnumerable<VeiculoViewModel>>(veiculos));
    }

    [HttpGet("empresa/{id:guid}")]
    public async Task<ActionResult<IEnumerable<VeiculoViewModel>>> ObterTodosVeiculosPorEmpresa(
        [FromRoute] Guid id)
    {
        var veiculos = await _repository.ObterTodosVeiculoComHorariosPorEmpresa(id);

        if (veiculos == null || !veiculos.Any())
            await Notify(HttpStatusCode.NotFound.ToString(), "");

        return await CustomResponse(_mapper.Map<IEnumerable<VeiculoViewModel>>(veiculos));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VeiculoViewModel>> ObterVeiculoPorId([FromRoute] Guid id)
    {
        var veiculo = await _repository.ObterVeiculoComHorarios(id);

        if (veiculo == null)
            await Notify(HttpStatusCode.NotFound.ToString(), "");

        return await CustomResponse(_mapper.Map<VeiculoViewModel>(veiculo));
    }

    [HttpPost("registrar-entrada")]
    public async Task<ActionResult> RegistrarEntradaVeiculo(
        [FromBody] VeiculoDto model)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        var veiculo = new Veiculo(model.EmpresaId, model.Marca, model.Modelo, model.Cor, model.Placa, model.Ano, model.Tipo);
        var horario = new RegistroEstacionamento(veiculo.Id, TimeOnly.FromDateTime(DateTime.Now));

        await _service.RegistrarEntradaVeiculo(veiculo, horario);

        return await CustomResponse(new
        {
            Marca = veiculo.Marca,
            Modelo = veiculo.Modelo,
            Placa = veiculo.Placa
        });
    }

    [HttpPatch("empresa/{id:guid}/{idVeiculo:guid}")]
    public async Task<ActionResult<VeiculoViewModel>> RegistrarSaidaVeiculo(
        [FromRoute] Guid id,
        [FromRoute] Guid idVeiculo)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        await _service.RegistrarSaidaVeiculo(id, idVeiculo);

        return await CustomResponse();
    }
}

