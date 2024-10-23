using ToochiChat.Domain.Models.Auth;

namespace ToochiChat.Application.Auth.Interfaces.Infrastructure;

public interface IJwtProvider
{
    /// <summary>
    /// Generates new JWT-token
    /// </summary>
    /// <param name="account">Account domain model</param>
    /// <returns>JWT-token</returns>
    public string GenerateToken(Account account);
}