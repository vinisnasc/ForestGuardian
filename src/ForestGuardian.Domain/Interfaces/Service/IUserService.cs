using System.Security.Claims;

namespace ForestGuardian.Domain.Interfaces.Service;

public interface IUserService
{
    string Name { get; }
    Guid GetUserId();
    string GetUserEmail();
    bool IsAuthenticated();
    bool IsInRole(string role);
    IEnumerable<Claim> GetClaimsIdentity();
    string GetBaseUrl();
}