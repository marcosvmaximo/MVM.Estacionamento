using System;
namespace MVM.Estacionamento.Core;

public interface IMediatorHandler
{
    Task PublicarNotificacao<T>(T notificacao) where T : Notification;
}

