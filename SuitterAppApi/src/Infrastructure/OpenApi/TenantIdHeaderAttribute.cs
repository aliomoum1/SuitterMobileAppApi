using SuitterAppApi.Shared.Multitenancy;

namespace SuitterAppApi.Infrastructure.OpenApi;
public class TenantIdHeaderAttribute : SwaggerHeaderAttribute
{
    public TenantIdHeaderAttribute()
        : base(
            MultitenancyConstants.TenantIdName,
            "Input your tenant Id to access this API",
            string.Empty,
            true)
    {
    }
}
