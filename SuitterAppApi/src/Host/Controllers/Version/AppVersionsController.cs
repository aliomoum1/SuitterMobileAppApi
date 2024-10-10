using SuitterAppApi.Application.Version.AppVersions;
using SuitterAppApi.Application.Version.AppVersions.Dtos;

namespace SuitterAppApi.Host.Controllers.Version;
public class AppVersionsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.AppVersions)]
    [OpenApiOperation("Search appVersions using available filters.", "")]
    public Task<PaginationResponse<AppVersionDto>> SearchAsync(SearchAppVersionsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.AppVersions)]
    [OpenApiOperation("Get appVersion details.", "")]
    public Task<AppVersionDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetAppVersionRequest(id));
    }
    [AllowAnonymous]
    [TenantIdHeader]
    [ApiConventionMethod(typeof(ApiConventions), nameof(ApiConventions.Get))]
    [HttpGet("GetLastAppVersion")]
    [OpenApiOperation("Get Last App Version.", "")]
    public Task<string> GetLastAppVersion()
    {
        return Mediator.Send(new GetLastVersionRequest());
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.AppVersions)]
    [OpenApiOperation("Get appVersion details via dapper.", "")]
    public Task<AppVersionDto> GetDapperAsync(Guid id)
    {
        return Mediator.Send(new GetAppVersionViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.AppVersions)]
    [OpenApiOperation("Create a new appVersion.", "")]
    public Task<Guid> CreateAsync(CreateAppVersionRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.AppVersions)]
    [OpenApiOperation("Update a appVersion.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateAppVersionRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.AppVersions)]
    [OpenApiOperation("Delete a appVersion.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteAppVersionRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.AppVersions)]
    [OpenApiOperation("Export a appVersions.", "")]
    public async Task<FileResult> ExportAsync(ExportAppVersionsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "AppVersionExports");
    }
}
