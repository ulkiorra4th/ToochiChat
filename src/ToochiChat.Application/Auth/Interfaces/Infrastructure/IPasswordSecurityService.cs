namespace ToochiChat.Application.Auth.Interfaces.Infrastructure;

public interface IPasswordSecurityService
{ 
    /// <summary>
    /// Hashes password using the salt
    /// </summary>
    /// <param name="password">password</param>
    /// <param name="salt">salt</param>
    /// <returns>hashed password</returns>
    public string HashPassword(string password, string salt);
    
    /// <summary>
    /// Checks if the hash of entered password equals stored hashed password
    /// </summary>
    /// <param name="password">entered password</param>
    /// <param name="salt">stored account's salt</param>
    /// <param name="hashedPassword">stored hashed password</param>
    /// <returns>The result of comparing</returns>
    public bool Verify(string password, string salt,  string hashedPassword);
    
    /// <summary>
    /// Generates salt to hash passwords
    /// </summary>
    /// <returns>salt</returns>
    public string GenerateSalt();
    
    /// <summary>
    /// Generates verification code
    /// </summary>
    /// <returns>verification code</returns>
    public string GenerateVerificationCode();
}