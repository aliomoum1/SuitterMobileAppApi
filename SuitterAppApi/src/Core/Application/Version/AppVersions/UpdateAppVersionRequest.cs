using SuitterAppApi.Domain.Version;
namespace SuitterAppApi.Application.Version.AppVersions;
public class UpdateAppVersionRequest : IRequest<Guid>
{
    public string Number { get; set; }
    public string Note { get; set; }
    public Guid Id { get; set; }
}
public class UpdateAppVersionRequestHandler : IRequestHandler<UpdateAppVersionRequest, Guid>
{
    private readonly IRepository<AppVersion> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;
    public UpdateAppVersionRequestHandler(IRepository<AppVersion> repository, IStringLocalizer<UpdateAppVersionRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _t, _file) = (repository, localizer, file);
    public async Task<Guid> Handle(UpdateAppVersionRequest request, CancellationToken cancellationToken)
    {
        var appVersion = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = appVersion ?? throw new NotFoundException(_t["AppVersion {0} Not Found.", request.Id]);
        var updatedAppVersion = appVersion.Update(request.Number, request.Note );
        await _repository.UpdateAsync(updatedAppVersion, cancellationToken);
        return request.Id;
    }
}
