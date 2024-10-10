

namespace SuitterAppApi.Application.Folder.Change22s;

public class UpdateChange22Request : IRequest<Guid>
{
    prop
}

public class UpdateChange22RequestHandler : IRequestHandler<UpdateChange22Request, Guid>
{
    private readonly IRepository<Change22> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdateChange22RequestHandler(IRepository<Change22> repository, IStringLocalizer<UpdateChange22RequestHandler> localizer, IFileStorageService file) =>
        (_repository, _t, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(UpdateChange22Request request, CancellationToken cancellationToken)
    {
        var change22 = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = change22 ?? throw new NotFoundException(_t["Change22 {0} Not Found.", request.Id]);

        // Remove old image if flag is set

        imChange


        var updatedChange22 = change22.Update(zazazaz);

        // Add Domain Events to be raised after the commit
        change22.DomainEvents.Add(EntityUpdatedEvent.WithEntity(change22));

        await _repository.UpdateAsync(updatedChange22, cancellationToken);

        return request.Id;
    }
}