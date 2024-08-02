using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Application.Auth.Interfaces;
using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Auth;

namespace ToochiChat.Application.Tests.Auth;

public sealed class AccountServiceTest
{
    private const string Chars = "abcdefghijklmnopqrstuvwxyz";
    private const string InvalidChars = @"!@#$%^&*()+=<>?/\|!@#$%'`~ ";
    private const string Numbers = "0123456789";
    private const int CountOfGeneratedTestData = 10;
    private const int EmailLength = 12;
    private const int PasswordLength = 16;
    
    private static readonly Random Random = new Random(Environment.TickCount);
    private readonly IAccountService _accountService;
    
    public AccountServiceTest()
    {
        _accountService = Services.Provider.GetRequiredService<IAccountService>();
    }
    
    #region RegisterMethodTests

    [Theory]
    [MemberData(nameof(AccountRegisterData))]
    public async Task RegisterEmailExistTest(string email, string password, DateTime birthDate)
    {
        TestData.Accounts.Add(Account.Create(Guid.NewGuid(), email, password, "salt", "token",
            User.CreateDefault(birthDate).Value).Value);
        
        var tokenResult = await _accountService.Register(email, password, birthDate);
        Assert.Equal(Result.Failure<string>($"user with email {email} already exists"), tokenResult);
    }

    [Theory]
    [MemberData(nameof(AccountRegisterData))]
    public async Task RegisterSuccessTest(string email, string password, DateTime birthDate)
    {
        var tokenResult = await _accountService.Register(email, password, birthDate);
        Assert.True(tokenResult.IsSuccess);
    }

    [Theory]
    [MemberData(nameof(InvalidBirthDateAccountRegisterData))]
    public async Task InvalidBirthDateRegisterFailureTest(string email, string password, DateTime birthDate)
    {
        var tokenResult = await _accountService.Register(email, password, birthDate);
        Assert.False(tokenResult.IsSuccess);
    }
    
    [Theory]
    [MemberData(nameof(InvalidPasswordAccountRegisterData))]
    public async Task InvalidPasswordRegisterFailureTest(string email, string password, DateTime birthDate)
    {
        var tokenResult = await _accountService.Register(email, password, birthDate);
        Assert.False(tokenResult.IsSuccess);
    }
    
    [Theory]
    [MemberData(nameof(InvalidEmailAccountRegisterData))]
    public async Task InvalidEmailRegisterFailureTest(string email, string password, DateTime birthDate)
    {
        var tokenResult = await _accountService.Register(email, password, birthDate);
        Assert.False(tokenResult.IsSuccess);
    }
    
    #endregion

    #region LoginMethodTests

    [Theory]
    [MemberData(nameof(AccountLoginData))]
    public async Task LoginFailureTest(string email, string password)
    {
        var tokenResult = await _accountService.LogIn(email, password);
        Assert.False(tokenResult.IsSuccess);
    }
    
    #endregion

    #region Data
    
    public static IEnumerable<object[]> AccountRegisterData()
    {
        for (int i = 0; i < CountOfGeneratedTestData; i++)
        {
            var email = GenerateEmail();
            var password = GeneratePassword();
            var birthDate = GenerateBirthDate(1980, 2005);
            
            yield return new object[] { email, password, birthDate };
        }
    }
    
    public static IEnumerable<object[]> AccountLoginData()
    {
        for (int i = 0; i < CountOfGeneratedTestData; i++)
        {
            var email = GenerateEmail();
            var password = GeneratePassword();

            yield return new object[] { email, password};
        }
    }
    
    public static IEnumerable<object[]> InvalidBirthDateAccountRegisterData()
    {
        for (int i = 0; i < CountOfGeneratedTestData; i++)
        {
            var email = GenerateEmail();
            var password = GeneratePassword();
            var birthDate = GenerateBirthDate(DateTime.Now.Year, DateTime.Now.Year + 100);
            
            yield return new object[] { email, password, birthDate };
        }
    }
    
    public static IEnumerable<object[]> InvalidPasswordAccountRegisterData()
    {
        for (int i = 0; i < CountOfGeneratedTestData; i++)
        {
            var email = GenerateEmail();
            var password = GeneratePassword(isInvalid: true);
            var birthDate = GenerateBirthDate(1980, 2005);
            
            yield return new object[] { email, password, birthDate };
        }
    }
    
    public static IEnumerable<object[]> InvalidEmailAccountRegisterData()
    {
        for (int i = 0; i < CountOfGeneratedTestData; i++)
        {
            var email = GenerateEmail(isInvalid: true);
            var password = GeneratePassword();
            var birthDate = GenerateBirthDate(1980, 2005);
            
            yield return new object[] { email, password, birthDate };
        }
    }

    private static string GenerateEmail(bool isInvalid = false)
    {
        var randomString = new StringBuilder(EmailLength);
        for (int j = 0; j < EmailLength; ++j)
            randomString.Append(Chars[Random.Next(Chars.Length)]);

        if (isInvalid) randomString.Append(InvalidChars[Random.Next(InvalidChars.Length)]);

        return randomString + "@gmail.com";
    }

    private static string GeneratePassword(bool isInvalid = false)
    {
        var randomString = new StringBuilder(PasswordLength);
        for (int j = 0; j < PasswordLength / 2 - 1; ++j)
            randomString.Append(Chars[Random.Next(Chars.Length)]);

        randomString.Append(Numbers[Random.Next(Numbers.Length)]);
        
        var password = randomString.ToString() + randomString.ToString().ToUpper();
        return isInvalid ? password.ToLower() : password;
    }

    private static DateTime GenerateBirthDate(int minYear, int maxYear)
    {
        return new DateTime(Random.Next(minYear, maxYear), Random.Next(1, 12), Random.Next(1, 28));
    }
    
    #endregion
}