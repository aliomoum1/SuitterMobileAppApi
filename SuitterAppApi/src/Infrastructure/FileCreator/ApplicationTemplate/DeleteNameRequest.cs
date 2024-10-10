
namespace SuitterAppApi.Application.Folder.Change22s;

public class DeleteChange22Request : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteChange22Request(Guid id) => Id = id;
}

public class DeleteChange22RequestHandler : IRequestHandler<DeleteChange22Request, Guid>
{
    private readonly IRepository<Change22> _repository;
    private readonly IStringLocalizer _t;

    public DeleteChange22RequestHandler(IRepository<Change22> repository, IStringLocalizer<DeleteChange22RequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteChange22Request request, CancellationToken cancellationToken)
    {
        var change22 = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = change22 ?? throw new NotFoundException(_t["Change22 {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        change22.DomainEvents.Add(EntityDeletedEvent.WithEntity(change22));

        await _repository.DeleteAsync(change22, cancellationToken);

        return request.Id;
    }
}