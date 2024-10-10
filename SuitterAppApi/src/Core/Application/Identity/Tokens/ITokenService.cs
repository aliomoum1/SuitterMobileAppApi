namespace SuitterAppApi.Application.Identity.Tokens;

public interface ITokenService : ITransientService
{
    Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress, CancellationToken cancellationToken);
    Task<TokenResponse> GetTokenAsync(TokenByMobileRequest request, string ipAddress, CancellationToken cancellationToken);

    Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
    Task<TokenResponse> RefreshTokenAsync(RefreshTokenFromMobileRequest request, string ipAddress);
}