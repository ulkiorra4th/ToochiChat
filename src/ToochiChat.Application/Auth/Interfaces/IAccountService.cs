using CSharpFunctionalExtensions;

namespace ToochiChat.Application.Auth.Interfaces;

public interface IAccountService
{
    /// <summary>
    /// Registers a new account in system. Logs in and sends a verification code
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="birthDate"></param>
    /// <returns>auth token</returns>
    public Task<Result<string>> Register(string email, string password, DateTime birthDate);
    
    /// <summary>
    /// logs account in
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns>auth token</returns>
    public Task<Result<string>> LogIn(string email, string password);
    
    /// <summary>
    /// verifies account
    /// </summary>
    /// <param name="email"></param>
    /// <param name="verificationCode"></param>
    /// <returns>verification state</returns>
    public Task<Result<bool>> Verify(string email, string verificationCode);
    
    /// <summary>
    /// Sends unique verification code by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    string SendVerificationCode(string email);
}