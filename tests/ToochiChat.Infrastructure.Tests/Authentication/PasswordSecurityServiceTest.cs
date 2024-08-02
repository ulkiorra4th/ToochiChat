using System.Text;
using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Application.Auth.Interfaces.Infrastructure;

namespace ToochiChat.Infrastructure.Tests.Authentication;

public sealed class PasswordSecurityServiceTest
{
    private const string Chars = "abcdefghijklmnopqrstuvwxyz";
    private const string Numbers = "0123456789";
    private const int PasswordLength = 16;
    private const int CountOfGeneratedTestData = 15;
    
    private static readonly Random Random = new Random(Environment.TickCount);

    private readonly IPasswordSecurityService _passwordSecurityService;

    public PasswordSecurityServiceTest()
    {
        _passwordSecurityService = Services.Provider.GetRequiredService<IPasswordSecurityService>();
    }

    [Theory]
    [MemberData(nameof(PasswordsData))]
    public void VerifyTest(string password)
    {
        var salt = _passwordSecurityService.GenerateSalt();
        var hashedPassword = _passwordSecurityService.HashPassword(password, salt);
        
        Assert.True(_passwordSecurityService.Verify(password, salt, hashedPassword));
    }

    #region Data
    
    public static IEnumerable<object[]> PasswordsData()
    {
        for (int i = 0; i < CountOfGeneratedTestData; i++)
        {
            var randomString = new StringBuilder(PasswordLength);
            for (int j = 0; j < PasswordLength / 2 - 1; ++j)
                randomString.Append(Chars[Random.Next(Chars.Length)]);

            randomString.Append(Numbers[Random.Next(Numbers.Length)]);
            
            var password = randomString.ToString() + randomString.ToString().ToUpper();

            yield return new object[] { password };
        }
    }
    
    #endregion
}