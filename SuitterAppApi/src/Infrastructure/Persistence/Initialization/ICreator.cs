namespace SuitterAppApi.Infrastructure.Persistence.Initialization;

public interface ICreator
{
    Task InitializeAsync(CancellationToken cancellationToken);
}