namespace ToochiChat.Application.Auth.Interfaces.Infrastructure;

public interface IPasswordSecurityService
{ 
    public string HashPassword(string password, string salt);
    public bool Verify(string password, string salt,  string hashedPassword);
    public string GenerateSalt();
    public string GenerateVerificationCode();
}