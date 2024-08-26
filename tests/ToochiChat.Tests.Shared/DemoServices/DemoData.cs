using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Auth;

namespace ToochiChat.Tests.Shared.DemoServices;

internal static class DemoData
{
    public static List<Account> Accounts { get; } = new()
    {
        Account.Create(Guid.NewGuid(), "ramin.muhtarov@gmail.com", "123", "123", 
            User.Create("as", new DateTime(1000, 10, 10)).Value).Value
    };
}