namespace SuitterAppApi.Application.Folder.Change22s;

public class GetChange22Request : IRequest<Change22DetailsDto>
{
    public Guid Id { get; set; }

    public GetChange22Request(Guid id) => Id = id;
}

public class GetChange22RequestHandler : IRequestHandler<GetChange22Request, Change22DetailsDto>
{
    private readonly IRepository<Change22> _repository;
    private readonly IStringLocalizer _t;

    public GetChange22RequestHandler(IRepository<Change22> repository, IStringLocalizer<GetChange22RequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Change22DetailsDto> Handle(GetChange22Request request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync((ISpecification<Change22, Change22DetailsDto>) new Change22ByIdSpec(request.Id), cancellationToken)?? throw new NotFoundException(_t["Change22 {0} Not Found.", request.Id]);
        
        
}