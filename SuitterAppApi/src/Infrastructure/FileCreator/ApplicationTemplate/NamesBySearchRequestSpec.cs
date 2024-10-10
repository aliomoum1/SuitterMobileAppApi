namespace SuitterAppApi.Application.Folder.Change22s;

public class Change22sBySearchRequestSpec : EntitiesByPaginationFilterSpec<Change22, Change22Dto>
{
    public Change22sBySearchRequestSpec(SearchChange22sRequest request): base(request) => Query.OrderBy(c => c.Name, !request.HasOrderBy());


}