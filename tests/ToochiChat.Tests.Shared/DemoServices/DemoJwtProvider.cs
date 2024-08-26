using ToochiChat.Application.Auth.Interfaces.Infrastructure;
using ToochiChat.Domain.Models.Auth;

namespace ToochiChat.Tests.Shared.DemoServices;

internal sealed class DemoJwtProvider : IJwtProvider
{
    public string GenerateToken(Account account) => account.GetHashCode().ToString();
}