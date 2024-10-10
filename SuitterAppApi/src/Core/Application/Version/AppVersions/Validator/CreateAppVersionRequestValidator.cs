using SuitterAppApi.Application.Version.AppVersions.Spec;
using SuitterAppApi.Domain.Version;
namespace SuitterAppApi.Application.Version.AppVersions.Validator;
public class CreateAppVersionRequestValidator : CustomValidator<CreateAppVersionRequest>
{
    public CreateAppVersionRequestValidator(IReadRepository<AppVersion> appVersionRepo, IStringLocalizer<CreateAppVersionRequestValidator> T)
    {
        RuleFor(x => x.Number)
    .NotEmpty().WithMessage("Version number cannot be empty.")
    .Matches(@"^\d+\.\d+\.\d+$").WithMessage("Version number must be in the format X.Y.Z, where X, Y, and Z are numbers.")
    .MaximumLength(256).WithMessage("Version number cannot be longer than 256 characters.");
        RuleFor(x => x.Note).NotEmpty().WithMessage("Note cannot be empty.");

        RuleFor(x => x.Number)
    .MustAsync(async (number, cancellation) =>
    {
        // Get the latest version from the repository
        var latestVersion = await appVersionRepo.FirstOrDefaultAsync(
            new GetLastVersionSpec(), // You can create a specification to get the latest version
            cancellationToken: cancellation
        );

        if (latestVersion == null)
        {
            // If no versions exist, allow the new version to be added
            return true;
        }

        // Compare version numbers (convert both current and latest version strings into Version objects)
        var newVersion = new System.Version(number);
        var lastVersion = new System.Version(latestVersion.Number);

        // The new version must be greater than the last version
        return newVersion > lastVersion;
    })
    .WithMessage("The version number must be greater than the last version.");
    }
}
