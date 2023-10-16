using System;
using FluentValidation;
using FluentValidation.Results;
using MVM.Estacionamento.Core;
namespace MVM.Estacionamento.Business.Services.Common;

public abstract class BaseService
{
    private readonly INotifyBus _bus;

    public BaseService(INotifyBus bus)
    {
        _bus = bus;
    }

    protected bool Validar(Entity entity)
    {
        entity.Validar();

        if (!entity.ValidationResult.IsValid)
        {
            Notificar(entity.ValidationResult);
            return false;
        }

        return true;
    }

    protected virtual void Notificar(ValidationResult result)
    {
        foreach (ValidationFailure error in result.Errors)
        {
            _bus.PublicarNotificacao(new Notification(error.PropertyName, error.ErrorMessage));
        }
    }

    protected virtual void Notificar(Notification notification)
    {
        _bus.PublicarNotificacao(notification);
    }

    protected virtual void Notificar(string key, string value)
    {
        _bus.PublicarNotificacao(new Notification(key, value));
    }
}

