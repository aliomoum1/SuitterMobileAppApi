using SuitterAppApi.Application.Version.AppVersions.Dtos;
using SuitterAppApi.Domain.Version;
namespace SuitterAppApi.Application.Version.AppVersions.Spec;
public class AppVersionByIdSpec : Specification<AppVersion, AppVersionDetailsDto>, ISingleResultSpecification
{
    public AppVersionByIdSpec(DefaultIdType id) => Query.Where(p => p.Id == id);
}
