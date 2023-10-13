using System;

namespace MVM.Estacionamento.Core;

public class NotifyBus : INotifyBus
{
    private List<Notification> _notifications;

    public NotifyBus()
    {
        _notifications = new List<Notification>();
    }

    public async Task PublicarNotificacao<T>(T notificacao) where T : Notification
    {
        _notifications.Add(notificacao);
    }

    public async Task<bool> AnyNotifications()
    {
        return _notifications.Any();
    }

    public async Task<IEnumerable<Notification>> GetNotifications()
    {
        return _notifications;
    }
}

