using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Auth;

namespace ToochiChat.Application.Tests;

internal static class TestData
{
    public static List<Account> Accounts { get; } = new()
    {
        Account.Create(Guid.NewGuid(), "123123@gmail.com", "123", "123", "some_token",
            User.Create("as", new DateTime(1000, 10, 10)).Value).Value
    };
}