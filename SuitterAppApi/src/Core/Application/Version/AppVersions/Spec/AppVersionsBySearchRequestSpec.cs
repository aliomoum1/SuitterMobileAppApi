using SuitterAppApi.Application.Version.AppVersions.Dtos;
using SuitterAppApi.Domain.Version;
namespace SuitterAppApi.Application.Version.AppVersions.Spec;
public class AppVersionsBySearchRequestSpec : EntitiesByPaginationFilterSpec<AppVersion, AppVersionDto>
{
    public AppVersionsBySearchRequestSpec(SearchAppVersionsRequest request) : base(request) => Query.OrderBy(c => c.CreatedOn, !request.HasOrderBy());
}
