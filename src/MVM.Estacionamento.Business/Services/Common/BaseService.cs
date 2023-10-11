using System;
using FluentValidation;
using FluentValidation.Results;
using MVM.Estacionamento.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVM.Estacionamento.Business.Services.Common;

public abstract class BaseService
{
    private readonly IMediatorHandler _bus;

    public BaseService(IMediatorHandler bus)
    {
        _bus = bus;
    }

    public virtual void Notificar(ValidationResult result)
    {
        foreach (ValidationFailure error in result.Errors)
        {
            _bus.PublicarNotificacao(new Notification(error.PropertyName, error.ErrorMessage));
        }
    }

    public virtual void Notificar(Notification notification)
    {
        _bus.PublicarNotificacao(notification);
    }

    public virtual void Notificar(string key, string value)
    {
        _bus.PublicarNotificacao(new Notification(key, value));
    }
}

