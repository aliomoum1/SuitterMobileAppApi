using SuitterAppApi.Application.Folder.Change22s;

namespace SuitterAppApi.Host.Controllers.Folder;
public class Change22sController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Change22s)]
    [OpenApiOperation("Search change22s using available filters.", "")]
    public Task<PaginationResponse<Change22Dto>> SearchAsync(SearchChange22sRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Change22s)]
    [OpenApiOperation("Get change22 details.", "")]
    public Task<Change22DetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetChange22Request(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Change22s)]
    [OpenApiOperation("Get change22 details via dapper.", "")]
    public Task<Change22Dto> GetDapperAsync(Guid id)
    {
        return Mediator.Send(new GetChange22ViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Change22s)]
    [OpenApiOperation("Create a new change22.", "")]
    public Task<Guid> CreateAsync(CreateChange22Request request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Change22s)]
    [OpenApiOperation("Update a change22.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateChange22Request request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Change22s)]
    [OpenApiOperation("Delete a change22.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteChange22Request(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Change22s)]
    [OpenApiOperation("Export a change22s.", "")]
    public async Task<FileResult> ExportAsync(ExportChange22sRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "Change22Exports");
    }
}