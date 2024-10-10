using SuitterAppApi.Infrastructure.Multitenancy;

namespace SuitterAppApi.Infrastructure.Persistence.Initialization;
internal interface IDatabaseInitializer
{
    Task InitializeDatabasesAsync(CancellationToken cancellationToken);
    Task InitializeApplicationDbForTenantAsync(FSHTenantInfo tenant, CancellationToken cancellationToken);
}