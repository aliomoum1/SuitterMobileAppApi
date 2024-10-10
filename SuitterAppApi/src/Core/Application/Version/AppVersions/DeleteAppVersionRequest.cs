using SuitterAppApi.Domain.Version;
namespace SuitterAppApi.Application.Version.AppVersions;
public class DeleteAppVersionRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public DeleteAppVersionRequest(Guid id) => Id = id;
}
public class DeleteAppVersionRequestHandler : IRequestHandler<DeleteAppVersionRequest, Guid>
{
    private readonly IRepository<AppVersion> _repository;
    private readonly IStringLocalizer _t;
    public DeleteAppVersionRequestHandler(IRepository<AppVersion> repository, IStringLocalizer<DeleteAppVersionRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);
    public async Task<Guid> Handle(DeleteAppVersionRequest request, CancellationToken cancellationToken)
    {
        var appVersion = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = appVersion ?? throw new NotFoundException(_t["AppVersion {0} Not Found."]);
        await _repository.DeleteAsync(appVersion, cancellationToken);
        return request.Id;
    }
}
