using System.Security.Claims;

namespace SuitterAppApi.Infrastructure.Auth;
public interface ICurrentUserInitializer
{
    void SetCurrentUser(ClaimsPrincipal user);

    void SetCurrentUserId(string userId);
}