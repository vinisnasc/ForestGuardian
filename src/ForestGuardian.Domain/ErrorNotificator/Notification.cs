using System.Net;

namespace ForestGuardian.Domain.ErrorNotificator;

public class Notification
{
    public Notification(string message)
        : this(message, HttpStatusCode.BadRequest)
    {
    }

    public Notification(string message, HttpStatusCode statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }

    public string Message { get; }
    public HttpStatusCode StatusCode { get; set; }
}