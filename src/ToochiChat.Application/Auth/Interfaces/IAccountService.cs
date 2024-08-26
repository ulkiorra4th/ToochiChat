using CSharpFunctionalExtensions;

namespace ToochiChat.Application.Auth.Interfaces;

public interface IAccountService
{
    public Task<Result<string>> Register(string email, string password, DateTime birthDate);
    public Task<Result<string>> LogIn(string email, string password);
    public Task<Result<bool>> Verify(string email, string verificationCode);
    string SendVerificationCode(string email);
}