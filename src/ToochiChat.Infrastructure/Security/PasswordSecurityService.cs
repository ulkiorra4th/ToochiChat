using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ToochiChat.Application.Auth.Interfaces.Infrastructure;

namespace ToochiChat.Infrastructure.Security;

internal sealed class PasswordSecurityService : IPasswordSecurityService
{
    private const int SaltByteArrayLength = 26;
    private const int IterationCount = 5000;
    private const int NumBytesRequested = 64;
    
    private readonly RandomNumberGenerator _randomNumberGenerator;

    public PasswordSecurityService(RandomNumberGenerator randomNumberGenerator)
    {
        _randomNumberGenerator = randomNumberGenerator;
    }
    
    public string HashPassword(string password, string salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            System.Text.Encoding.ASCII.GetBytes(salt),
            KeyDerivationPrf.HMACSHA512,
            IterationCount,
            NumBytesRequested
        ));
    }

    public bool Verify(string password, string salt, string hashedPassword)
    {
        return HashPassword(password, salt) == hashedPassword;
    }
    
    public string GenerateSalt()
    {
        var encBytes = new byte[SaltByteArrayLength];
        _randomNumberGenerator.GetBytes(encBytes);

        return Convert.ToBase64String(encBytes);
    }

    public string GenerateVerificationCode() => 
        RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
}