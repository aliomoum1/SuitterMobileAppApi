namespace SuitterAppApi.Application.Version.AppVersions.Dtos;
public class AppVersionExportDto : IDto
{
    public string Number { get; set; } = default!;
    public string Note { get; set; } = default!;
    public DefaultIdType CreatedBy { get; set; } = default!;
    public DateTime CreatedOn { get; set; } = default!;
    public DefaultIdType LastModifiedBy { get; set; } = default!;
    public DateTime? LastModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DefaultIdType? DeletedBy { get; set; }
}
