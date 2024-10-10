using SuitterAppApi.Application.Version.AppVersions.Spec;
using SuitterAppApi.Domain.Version;
namespace SuitterAppApi.Application.Version.AppVersions;
public class GetLastVersionRequest : IRequest<string>
{
}
public class GetLastVersionRequestHandler : IRequestHandler<GetLastVersionRequest, string>
{
    IReadRepository<Domain.Version.AppVersion> _versionRepo;
    public GetLastVersionRequestHandler(IReadRepository<AppVersion> versionRepo)
    {
        _versionRepo = versionRepo;
    }
    [Obsolete]
    public async Task<string> Handle(GetLastVersionRequest request, CancellationToken cancellationToken)
    {
        var lastVersion = await _versionRepo.GetBySpecAsync(new GetLastVersionSpec(), cancellationToken);
        if (lastVersion != null)
        {
            return lastVersion.Number;
        }
        else { return "No Version Added"; }
    }
}
