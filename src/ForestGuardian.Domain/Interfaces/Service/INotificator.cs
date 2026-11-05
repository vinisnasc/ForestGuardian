using ForestGuardian.Domain.ErrorNotificator;

namespace ForestGuardian.Domain.Interfaces.Service;

public interface INotificator
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notificacao);
}