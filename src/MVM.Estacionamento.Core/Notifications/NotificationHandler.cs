using System;
using MediatR;

namespace MVM.Estacionamento.Core;

public class NotificationHandler : INotificationHandler<Notification>
{
    private List<Notification> _notifications;

    public NotificationHandler()
    {
        _notifications = new List<Notification>();
    }

    public async Task Handle(Notification notification, CancellationToken cancellationToken)
    {
        _notifications.Add(notification);
    }

    public bool AnyNotifications()
    {
        return _notifications.Any();
    }

    public IEnumerable<Notification> GetNotifications()
    {
        return _notifications;
    }
}

