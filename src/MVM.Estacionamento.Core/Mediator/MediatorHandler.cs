using System;
using MediatR;

namespace MVM.Estacionamento.Core;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublicarNotificacao<T>(T notificacao) where T : Notification
    {
        await _mediator.Publish(notificacao);
    }
}

