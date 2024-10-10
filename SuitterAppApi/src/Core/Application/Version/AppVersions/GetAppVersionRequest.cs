using SuitterAppApi.Application.Version.AppVersions.Dtos;
using SuitterAppApi.Application.Version.AppVersions.Spec;
using SuitterAppApi.Domain.Version;
namespace SuitterAppApi.Application.Version.AppVersions;
public class GetAppVersionRequest : IRequest<AppVersionDetailsDto>
{
    public Guid Id { get; set; }
    public GetAppVersionRequest(Guid id) => Id = id;
}
public class GetAppVersionRequestHandler : IRequestHandler<GetAppVersionRequest, AppVersionDetailsDto>
{
    private readonly IRepository<AppVersion> _repository;
    private readonly IStringLocalizer _t;
    public GetAppVersionRequestHandler(IRepository<AppVersion> repository, IStringLocalizer<GetAppVersionRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);
    public async Task<AppVersionDetailsDto> Handle(GetAppVersionRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync((ISpecification<AppVersion, AppVersionDetailsDto>)new AppVersionByIdSpec(request.Id), cancellationToken) ?? throw new NotFoundException(_t["AppVersion {0} Not Found.", request.Id]);
}
