
using SuitterAppApi.Application.Common.Exporters;
using SuitterAppApi.Application.Version.AppVersions.Dtos;
using SuitterAppApi.Domain.Version;

namespace SuitterAppApi.Application.Version.AppVersions;

public class ExportAppVersionsRequest : BaseFilter, IRequest<Stream>
{

}

public class ExportAppVersionsRequestHandler : IRequestHandler<ExportAppVersionsRequest, Stream>
{
    private readonly IReadRepository<AppVersion> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportAppVersionsRequestHandler(IReadRepository<AppVersion> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportAppVersionsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportAppVersionsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportAppVersionsSpecification : EntitiesByBaseFilterSpec<AppVersion, AppVersionExportDto>
{
    public ExportAppVersionsSpecification(ExportAppVersionsRequest request) : base(request) { }


}
