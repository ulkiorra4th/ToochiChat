using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ToochiChat.Application.Auth.Interfaces.Infrastructure;

namespace ToochiChat.Infrastructure.Authentication;

internal sealed class PasswordSecurityService : IPasswordSecurityService
{
    private const int SALT_BYTE_ARRAY_LENGTH = 26;
    private const int ITERATION_COUNT = 5000;
    private const int NUM_BYTES_REQUESTED = 64;
    
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
            ITERATION_COUNT,
            NUM_BYTES_REQUESTED
        ));
    }

    public bool Verify(string password, string salt, string hashedPassword)
    {
        return HashPassword(password, salt) == hashedPassword;
    }
    
    public string GenerateSalt()
    {
        var encBytes = new byte[SALT_BYTE_ARRAY_LENGTH];
        _randomNumberGenerator.GetBytes(encBytes);

        return Convert.ToBase64String(encBytes);
    }
}