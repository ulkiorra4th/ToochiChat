using ToochiChat.Application.Auth.Interfaces.Infrastructure;
using ToochiChat.Domain.Models.Auth;

namespace ToochiChat.Application.Tests.Auth.TestServices;

internal sealed class TestJwtProvider : IJwtProvider
{
    public string GenerateToken(Account account) => account.GetHashCode().ToString();
}