
namespace SuitterAppApi.Application.Folder.Change22s;

public class CreateChange22Request : IRequest<Guid>
{
    prop
}

public class CreateChange22RequestHandler : IRequestHandler<CreateChange22Request, Guid>
{
    private readonly IRepository<Change22> _repository;
    private readonly IFileStorageService _file;

    public CreateChange22RequestHandler(IRepository<Change22> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateChange22Request request, CancellationToken cancellationToken)
    {

        replaceImage

        var change22 = new Change22(ctor);

        // Add Domain Events to be raised after the commit
        change22.DomainEvents.Add(EntityCreatedEvent.WithEntity(change22));

        await _repository.AddAsync(change22, cancellationToken);

        return change22.Id;
    }
}