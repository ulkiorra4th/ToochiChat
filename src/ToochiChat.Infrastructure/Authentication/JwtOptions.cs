namespace ToochiChat.Infrastructure.Authentication;

public sealed class JwtOptions
{
    public string SecretKey { get; set; }
    public int ExpiresHours { get; set; }
}