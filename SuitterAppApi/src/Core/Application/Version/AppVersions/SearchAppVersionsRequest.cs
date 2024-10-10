using SuitterAppApi.Application.Version.AppVersions.Dtos;
using SuitterAppApi.Application.Version.AppVersions.Spec;
using SuitterAppApi.Domain.Version;

namespace SuitterAppApi.Application.Version.AppVersions;
public class SearchAppVersionsRequest : PaginationFilter, IRequest<PaginationResponse<AppVersionDto>>
{
    public string Number { get; set; }
    public string Note { get; set; }
}
public class SearchAppVersionsRequestHandler : IRequestHandler<SearchAppVersionsRequest, PaginationResponse<AppVersionDto>>
{
    private readonly IReadRepository<AppVersion> _repository;
    public SearchAppVersionsRequestHandler(IReadRepository<AppVersion> repository) => _repository = repository;
    public async Task<PaginationResponse<AppVersionDto>> Handle(SearchAppVersionsRequest request, CancellationToken cancellationToken)
    {
        var spec = new AppVersionsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}
