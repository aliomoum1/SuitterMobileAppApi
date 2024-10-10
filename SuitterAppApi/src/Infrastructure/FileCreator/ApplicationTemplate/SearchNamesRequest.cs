namespace SuitterAppApi.Application.Folder.Change22s;

public class SearchChange22sRequest : PaginationFilter, IRequest<PaginationResponse<Change22Dto>>
{
    prop

}

public class SearchChange22sRequestHandler : IRequestHandler<SearchChange22sRequest, PaginationResponse<Change22Dto>>
{
    private readonly IReadRepository<Change22> _repository;

    public SearchChange22sRequestHandler(IReadRepository<Change22> repository) => _repository = repository;

    public async Task<PaginationResponse<Change22Dto>> Handle(SearchChange22sRequest request, CancellationToken cancellationToken)
    {
        
        var spec = new Change22sBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}