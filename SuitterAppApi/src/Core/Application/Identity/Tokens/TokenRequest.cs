namespace SuitterAppApi.Application.Identity.Tokens;

public record TokenRequest(string Email, string Password);

public record TokenByMobileRequest(string Phone, string Password);


public class TokenRequestValidator : CustomValidator<TokenRequest>
{
    public TokenRequestValidator(IStringLocalizer<TokenRequestValidator> T)
    {
        RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
                .WithMessage(T["Invalid Email Address."]);

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}

public class TokenByMobileRequestValidator : CustomValidator<TokenByMobileRequest>
{
    public TokenByMobileRequestValidator(IStringLocalizer<TokenByMobileRequestValidator> T)
    {
        RuleFor(x => x.Phone)
            .Cascade(CascadeMode.Stop) 
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9]{10,15}$") 
            .WithMessage("Phone number must be between 10 and 15 digits and can start with '+'.");

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}