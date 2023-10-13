using System;
namespace MVM.Estacionamento.Core;

public interface INotifyBus
{
    Task PublicarNotificacao<T>(T notificacao) where T : Notification;
    Task<bool> AnyNotifications();
    Task<IEnumerable<Notification>> GetNotifications();
}

