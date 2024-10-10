using SuitterAppApi.Domain.Version;
namespace SuitterAppApi.Application.Version.AppVersions;
public class CreateAppVersionRequest : IRequest<DefaultIdType>
{
    public string Number { get; set; }
    public string Note { get; set; }
}
public class CreateAppVersionRequestHandler : IRequestHandler<CreateAppVersionRequest, DefaultIdType>
{
    private readonly IRepository<AppVersion> _repository;
    private readonly IFileStorageService _file;
    public CreateAppVersionRequestHandler(IRepository<AppVersion> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);
    public async Task<DefaultIdType> Handle(CreateAppVersionRequest request, CancellationToken cancellationToken)
    {
        var appVersion = new AppVersion(request.Number, request.Note );
        await _repository.AddAsync(appVersion, cancellationToken);
        return appVersion.Id;
    }
}
