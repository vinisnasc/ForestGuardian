using ForestGuardian.Domain.ErrorNotificator;
using ForestGuardian.Domain.Interfaces.Service;
using System.Net;

namespace ForestGuardian.Services;

public class BaseService
{
    private readonly INotificator _notificador;

    protected BaseService(INotificator notificador)
    => _notificador = notificador;

    protected void Notificate(string message) => _notificador.Handle(new Notification(message));
    protected void Notificate(string message, HttpStatusCode statusCode) => _notificador.Handle(new Notification(message, statusCode));
}