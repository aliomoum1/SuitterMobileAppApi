namespace SuitterAppApi.Infrastructure.Identity;

public class OtpGenerator
{
    private static readonly Random _random = new Random();

    public static string GenerateOtp(int length = 6)
    {
        // Create a random number with the specified length
        return _random.Next((int)Math.Pow(10, length - 1), (int)Math.Pow(10, length)).ToString();
    }
}