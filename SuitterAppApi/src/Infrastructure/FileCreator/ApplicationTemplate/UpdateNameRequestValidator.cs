namespace SuitterAppApi.Application.Folder.Change22s;

public class UpdateChange22RequestValidator : CustomValidator<UpdateChange22Request>
{
    public UpdateChange22RequestValidator(IReadRepository<Change22> change22Repo, IStringLocalizer<UpdateChange22RequestValidator> T)
    {

    }
}