using Mapster;
using SuitterAppApi.Application.Version.AppVersions.Dtos;
using SuitterAppApi.Domain.Version;

namespace SuitterAppApi.Application.Version.AppVersions;

public class GetAppVersionViaDapperRequest : IRequest<AppVersionDto>
{
    public Guid Id { get; set; }

    public GetAppVersionViaDapperRequest(Guid id) => Id = id;
}

public class GetAppVersionViaDapperRequestHandler : IRequestHandler<GetAppVersionViaDapperRequest, AppVersionDto>
{

    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer _t;

    public GetAppVersionViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<GetAppVersionViaDapperRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<AppVersionDto> Handle(GetAppVersionViaDapperRequest request, CancellationToken cancellationToken)
    {
        var appVersion = await _repository.QueryFirstOrDefaultAsync<AppVersion>(
            $"SELECT * FROM Catalog.\"AppVersions\" WHERE \"Id\"  = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = appVersion ?? throw new NotFoundException(_t["AppVersion {0} Not Found.", request.Id]);

        // Using mapster here throws a nullreference exception because of the "BrandAppVersion" property
        // in AppVersionDto and the appVersion not having a Brand assigned.
        return new AppVersionDto
        {
            Number = appVersion.Number,
Note = appVersion.Note,
CreatedBy = appVersion.CreatedBy,
CreatedOn = appVersion.CreatedOn,
LastModifiedBy = appVersion.LastModifiedBy,
LastModifiedOn = appVersion.LastModifiedOn,
DeletedOn = appVersion.DeletedOn,
DeletedBy = appVersion.DeletedBy,
Id = appVersion.Id,

        };
    }
}
