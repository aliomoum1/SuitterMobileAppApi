using SuitterAppApi.Domain.Version;

namespace SuitterAppApi.Application.Version.AppVersions.Validator;

public class UpdateAppVersionRequestValidator : CustomValidator<UpdateAppVersionRequest>
{
    public UpdateAppVersionRequestValidator(IReadRepository<AppVersion> appVersionRepo, IStringLocalizer<UpdateAppVersionRequestValidator> T)
    {
        RuleFor(x => x.Number)
.NotEmpty().WithMessage("Version number cannot be empty.")
.Matches(@"^\d+\.\d+\.\d+$").WithMessage("Version number must be in the format X.Y.Z, where X, Y, and Z are numbers.")
.MaximumLength(256).WithMessage("Version number cannot be longer than 256 characters.");
        RuleFor(x => x.Note).NotEmpty().WithMessage("Note cannot be empty.");
    }
}
