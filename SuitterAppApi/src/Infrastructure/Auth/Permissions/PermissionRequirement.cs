using Microsoft.AspNetCore.Authorization;

namespace SuitterAppApi.Infrastructure.Auth.Permissions;
internal class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; private set; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}