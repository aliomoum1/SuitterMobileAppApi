
using SuitterAppApi.Application.Common.Exporters;

namespace SuitterAppApi.Application.Folder.Change22s;

public class ExportChange22sRequest : BaseFilter, IRequest<Stream>
{

}

public class ExportChange22sRequestHandler : IRequestHandler<ExportChange22sRequest, Stream>
{
    private readonly IReadRepository<Change22> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportChange22sRequestHandler(IReadRepository<Change22> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportChange22sRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportChange22sSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportChange22sSpecification : EntitiesByBaseFilterSpec<Change22, Change22ExportDto>
{
    public ExportChange22sSpecification(ExportChange22sRequest request) : base(request) { }


}