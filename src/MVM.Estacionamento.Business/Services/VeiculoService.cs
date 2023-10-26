using System;
using System.Net;
using MVM.Estacionamento.Business.Enum;
using MVM.Estacionamento.Business.Interfaces;
using MVM.Estacionamento.Business.Interfaces.VeiculoContext;
using MVM.Estacionamento.Business.Models;
using MVM.Estacionamento.Business.Services.Common;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Business.Services;

public class VeiculoService : BaseService, IVeiculoService
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IRegistroEstacionamentoRepository _registroEstacionamentoRepository;

    public VeiculoService(INotifyBus notifyBus,
                          IVeiculoRepository veiculoRepository,
                          IEmpresaRepository empresaRepository,
                          IRegistroEstacionamentoRepository registroEstacionamentoRepository) : base(notifyBus)
    {
        _veiculoRepository = veiculoRepository;
        _empresaRepository = empresaRepository;
        _registroEstacionamentoRepository = registroEstacionamentoRepository;
    }

    public async Task RegistrarEntradaVeiculo(Veiculo veiculo, RegistroEstacionamento horario)
    {
        var empresa = await _empresaRepository.ObterEmpresaComVeiculos(veiculo.EmpresaId);

        if (empresa == null)
        {
            Notificar(HttpStatusCode.NotFound.ToString(), "Empresa informada não foi encontrada");
            return;
        }

        if (!Validar(veiculo))
        {
            Notificar(nameof(Veiculo), "Veiculo informado é inválido");
            return;
        }

        if (!empresa.PodeEstacionar(veiculo))
        {
            Notificar(nameof(Veiculo), $"O estacionamento da empresa {empresa.Nome} está lotado");
            return;
        }

        var veiculoExistente = empresa.Veiculos.FirstOrDefault(x => x.Placa.ToLower() == veiculo.Placa.ToLower());

        if (veiculoExistente != null)
        {
            if (veiculoExistente.Status)
            {
                Notificar(nameof(Veiculo), "Veiculo inserido já se encontra no estacionamento.");
                return;
            }

            if (veiculo.Tipo == ETipoVeiculo.Moto || veiculo.Tipo == ETipoVeiculo.Bicicleta)
                empresa.QuantidadeVagasMotos--;
            else
                empresa.QuantidadeVagasCarros--;

            veiculoExistente.RegitrarEntradaEstacionamento(horario);

            // Está dando erro ao atualizar, pois estou adicionando objetos não salvos de outras tabelas, direto na atualização
            await _registroEstacionamentoRepository.Adicionar(horario);
            await _veiculoRepository.Atualizar(veiculoExistente);
            await _empresaRepository.Atualizar(empresa);

            return;
        }

        if (veiculo.Status)
        {
            Notificar(nameof(veiculoExistente), "Veiculo inserido já se encontra no estacionamento.");
            return;
        }

        if (veiculo.Tipo == ETipoVeiculo.Moto || veiculo.Tipo == ETipoVeiculo.Bicicleta)
            empresa.QuantidadeVagasMotos--;
        else
            empresa.QuantidadeVagasCarros--;

        empresa.AdicionarVeiculo(veiculo);
        veiculo.RegitrarEntradaEstacionamento(horario);

        await _veiculoRepository.Adicionar(veiculo);
        await _empresaRepository.Atualizar(empresa);
    }

    public async Task RegistrarSaidaVeiculo(Guid idEmpresa, Guid idVeiculo)
    {
        var empresa = await _empresaRepository.ObterPorId(idEmpresa);

        if (empresa == null)
        {
            Notificar(HttpStatusCode.NotFound.ToString(), "Empresa informada não foi encontrada");
            return;
        }

        var veiculo = await _veiculoRepository.ObterVeiculoComHorarios(idVeiculo);

        if (veiculo == null)
        {
            Notificar(HttpStatusCode.NotFound.ToString(), "Veiculo informado não foi encontrado");
            return;
        }

        if (!veiculo.Status)
        {
            Notificar(nameof(Veiculo), "Não é possível remover um veiculo que não se encontra no Estacionamento.");
            return;
        }

        var ultimoRegistroEstacionamento = veiculo.RegistrosEstacionamento.OrderByDescending(r => r.Data).FirstOrDefault();
        if (ultimoRegistroEstacionamento == null)
        {
            Notificar(nameof(RegistroEstacionamento), "Veiculo não possui nenhum registro recente");
            return;
        }

        ultimoRegistroEstacionamento.MarcarSaida();
        await _registroEstacionamentoRepository.Atualizar(ultimoRegistroEstacionamento);

        // Voltar estoque

        if (veiculo.Tipo == ETipoVeiculo.Moto || veiculo.Tipo == ETipoVeiculo.Bicicleta)
            empresa.QuantidadeVagasMotos++;
        else
            empresa.QuantidadeVagasCarros++;

        // Desativar veiculo
        veiculo.Status = false;

        await _veiculoRepository.Atualizar(veiculo);
        await _empresaRepository.Atualizar(empresa);
    }
}

