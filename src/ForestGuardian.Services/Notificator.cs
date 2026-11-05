using ForestGuardian.Domain.ErrorNotificator;
using ForestGuardian.Domain.Interfaces.Service;

namespace ForestGuardian.Services;

public class Notificator : INotificator
{
    private List<Notification> _notifications;

    public Notificator() => _notifications = new List<Notification>();

    public void Handle(Notification notificacao) => _notifications.Add(notificacao);

    public List<Notification> GetNotifications() => _notifications;

    public bool HasNotification() => _notifications.Any();
}