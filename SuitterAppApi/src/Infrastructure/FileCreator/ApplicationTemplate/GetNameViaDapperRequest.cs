using Mapster;

namespace SuitterAppApi.Application.Folder.Change22s;

public class GetChange22ViaDapperRequest : IRequest<Change22Dto>
{
    public Guid Id { get; set; }

    public GetChange22ViaDapperRequest(Guid id) => Id = id;
}

public class GetChange22ViaDapperRequestHandler : IRequestHandler<GetChange22ViaDapperRequest, Change22Dto>
{

    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer _t;

    public GetChange22ViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<GetChange22ViaDapperRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Change22Dto> Handle(GetChange22ViaDapperRequest request, CancellationToken cancellationToken)
    {
        var change22 = await _repository.QueryFirstOrDefaultAsync<Change22>(
            $"SELECT * FROM Catalog.\"Change22s\" WHERE \"Id\"  = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = change22 ?? throw new NotFoundException(_t["Change22 {0} Not Found.", request.Id]);

        // Using mapster here throws a nullreference exception because of the "BrandChange22" property
        // in Change22Dto and the change22 not having a Brand assigned.
        return new Change22Dto
        {
            DapperChange
        };
    }
}