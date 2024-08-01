using ToochiChat.Domain.Models.Auth;

namespace ToochiChat.Application.Auth.Interfaces.Infrastructure;

public interface IJwtProvider
{
    public string GenerateToken(Account account);
}