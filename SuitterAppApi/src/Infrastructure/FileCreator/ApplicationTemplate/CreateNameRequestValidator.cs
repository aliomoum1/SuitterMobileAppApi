

namespace SuitterAppApi.Application.Folder.Change22s;

public class CreateChange22RequestValidator : CustomValidator<CreateChange22Request>
{
    public CreateChange22RequestValidator(IReadRepository<Change22> change22Repo, IStringLocalizer<CreateChange22RequestValidator> T)
    {

    }
}