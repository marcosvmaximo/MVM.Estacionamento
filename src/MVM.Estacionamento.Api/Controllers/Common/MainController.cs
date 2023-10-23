using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MVM.Estacionamento.Api.Controllers.Common;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Api.Controllers;

[ApiController]
public class MainController : ControllerBase
{
    protected readonly INotifyBus _notifyBus;
    protected readonly IMapper _mapper;

    public MainController(INotifyBus notifyBus, IMapper mapper)
    {
        _notifyBus = notifyBus;
        _mapper = mapper;
    }

    protected async Task<ActionResult> CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
            await NotifyModelState(modelState);

        return await CustomResponse();
    }

    protected async Task<ActionResult> CustomResponse(object result = null)
    {
        if (await _notifyBus.AnyNotifications())
        {
            var notifications = await _notifyBus.GetNotifications();
            var notFoundNotification = notifications.FirstOrDefault(x => x.Key == HttpStatusCode.NotFound.ToString());

            if (notFoundNotification != null)
            {
                return NotFound(new BaseResponse
                {
                    HttpCode = 404,
                    Sucess = true,
                    Message = "Não foi encontrado nenhum resultado."
                });
            }

            return BadRequest(new BaseResponse
            {
                HttpCode = 400,
                Sucess = false,
                Message = "Ocorreu um erro ao enviar a requisição.",
                Result = _notifyBus.GetNotifications().Result.Select(x => new { Key = x.Key, Value = x.Value })
            }); ;
        }

        return Ok(new BaseResponse
        {
            HttpCode = 200,
            Sucess = true,
            Message = "Requisição enviada com sucesso.",
            Result = result
        });
    }

    protected async Task NotifyModelState(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);

        foreach (var erro in erros)
        {
            var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            await Notify(erroMsg);
        }
    }

    protected async Task Notify(string message)
    {
        await _notifyBus.PublicarNotificacao(new Notification(null, message));
    }

    protected async Task Notify(string key, string message)
    {
        await _notifyBus.PublicarNotificacao(new Notification(key, message));
    }
}

