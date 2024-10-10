using Microsoft.AspNetCore.Authorization;
using SuitterAppApi.Shared.Authorization;

namespace SuitterAppApi.Infrastructure.Auth.Permissions;
public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = FSHPermission.NameFor(action, resource);
}