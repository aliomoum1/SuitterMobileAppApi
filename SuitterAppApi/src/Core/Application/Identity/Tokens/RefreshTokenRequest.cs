namespace SuitterAppApi.Application.Identity.Tokens;

public record RefreshTokenRequest(string Token, string RefreshToken);

public record RefreshTokenFromMobileRequest(string Token, string RefreshToken);